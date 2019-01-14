﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rambler.Web.Models;
using Rambler.Web.Models.Youtube.LiveBroadcast;
using Rambler.Web.Models.Youtube.LiveChat;

namespace Rambler.Web.Services
{
    public class YoutubeService
    {
        private readonly UserService userService;
        private readonly ILogger<YoutubeService> logger;
        public IConfiguration configuration { get; }

        public YoutubeService(UserService userService, ILogger<YoutubeService> logger, IConfiguration configuration)
        {
            this.userService = userService;
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<HttpResponseMessage> Get(string request)
        {
            return await Get(request, string.Empty);
        }

        public async Task<HttpResponseMessage> Get(string request, string accessToken)
        {
            var client = new HttpClient();

            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await client.GetAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Post(string url, IList<KeyValuePair<string, string>> data)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(data);

                var response = await client.PostAsync(url, content);
                return response;
            }
        }

        public async Task<AccessToken> GetToken()
        {
            var user = await userService.GetCurrentUser();

            var token = user.AccessTokens
                .FirstOrDefault(x => x.ApiSource == ApiSource.Youtube);

            return token;
        }

        public bool IsValidToken(AccessToken token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            return token.Status == AccessTokenStatus.Ok
                   && !string.IsNullOrEmpty(token.access_token);
        }

        public async Task<bool> HasValidToken()
        {
            var token = await GetToken();

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            return token.Status == AccessTokenStatus.Ok
                   && !string.IsNullOrEmpty(token.access_token);
        }

        public async Task<LiveChatMessageList> GetLiveChatMessages(string liveChatId)
        {
            if (string.IsNullOrEmpty(liveChatId))
            {
                throw new ArgumentNullException(liveChatId);
            }

            var token = await GetToken();
            if (!IsValidToken(token))
            {
                return null;
            }

            var response = await Get(
                $"https://www.googleapis.com/youtube/v3/liveChat/messages?liveChatId={liveChatId}&part=id,snippet,authorDetails",
                token.access_token);

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"GetLiveChatMessages error: {(int)response.StatusCode} - {response.ReasonPhrase}",
                    response);
                return null;
            }

            var liveChatMessageList = JsonConvert.DeserializeObject<LiveChatMessageList>(content);
            return liveChatMessageList;
        }

        public ChatMessage MapToChatMessage(LiveChatMessage item)
        {
            return new ChatMessage
            {
                Author = item.AuthorDetails.displayName,
                Message = item.snippet.displayMessage,
                Date = item.snippet.publishedAt,
                Source = "Youtube",
                SourceMessageId = item.id,
                SourceAuthorId = item.AuthorDetails.channelId
            };
        }

        public async Task<LiveBroadcastItem> GetLiveBroadcast()
        {
            var token = await GetToken();
            if (token != null && token.Status == AccessTokenStatus.Expired && token.HasRefreshToken)
            {
                await RefreshToken(token);
            }

            if (!IsValidToken(token))
            {
                return null;
            }

            var response = await Get(
                "https://www.googleapis.com/youtube/v3/liveBroadcasts?part=snippet&broadcastType=persistent&mine=true",
                token.access_token);

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"GetLiveBroadcast error: {(int)response.StatusCode} - {response.ReasonPhrase}: {content}");
                return null;
            }

            var liveBroadcastList = JsonConvert.DeserializeObject<LiveBroadcastList>(content);
            if (liveBroadcastList?.items == null || !liveBroadcastList.items.Any())
            {
                return null;
            }

            return liveBroadcastList.items.FirstOrDefault();
        }

        public async Task RefreshToken(AccessToken token)
        {
            var data = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", configuration["Authentication:Google:ClientId"]),
                new KeyValuePair<string, string>("client_secret", configuration["Authentication:Google:ClientSecret"]),
                new KeyValuePair<string, string>("refresh_token", token.refresh_token),
                new KeyValuePair<string, string>("grant_type", "refresh_token")
            };

            var response = await Post("https://accounts.google.com/o/oauth2/token", data);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"Error refreshing token: {response.StatusCode} - {response.ReasonPhrase}\n{content}");
            }

            var accessToken = JsonConvert.DeserializeObject<AccessToken>(content);
            accessToken.Id = token.Id;
            await userService.UpdateToken(accessToken);
        }

    }
};