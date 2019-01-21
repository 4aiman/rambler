﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rambler.Web.Data;

namespace Rambler.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190121212104_AddedChannels")]
    partial class AddedChannels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("Rambler.Web.Models.AccessToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiSource");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<int>("UserId");

                    b.Property<string>("access_token");

                    b.Property<int>("expires_in");

                    b.Property<string>("refresh_token");

                    b.Property<string>("token_type");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccessTokens");
                });

            modelBuilder.Entity("Rambler.Web.Models.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("MaximumReputation");

                    b.Property<int?>("MinimumReputation");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Channels");

                    b.HasData(
                        new { Id = 1, Name = "All" },
                        new { Id = 2, Name = "Reader" },
                        new { Id = 3, Name = "OBS" },
                        new { Id = 4, Name = "TTS" }
                    );
                });

            modelBuilder.Entity("Rambler.Web.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Message");

                    b.Property<string>("Source");

                    b.Property<string>("SourceAuthorId");

                    b.Property<string>("SourceMessageId");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Rambler.Web.Models.ConfigurationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Key");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("ConfigurationSettings");

                    b.HasData(
                        new { Id = 1, Key = "Authentication:Google:ClientId", Name = "Youtube client id" },
                        new { Id = 2, Key = "Authentication:Google:ClientSecret", Name = "Youtube client secret" },
                        new { Id = 3, Key = "Authentication:Twitch:ClientId", Name = "Twitch client id" },
                        new { Id = 4, Key = "Authentication:Twitch:ClientSecret", Name = "Twitch client secret" }
                    );
                });

            modelBuilder.Entity("Rambler.Web.Models.Integration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Integrations");

                    b.HasData(
                        new { Id = 1, IsEnabled = false, Name = "Youtube" },
                        new { Id = 2, IsEnabled = false, Name = "Twitch" }
                    );
                });

            modelBuilder.Entity("Rambler.Web.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Rambler.Web.Models.AccessToken", b =>
                {
                    b.HasOne("Rambler.Web.Models.User", "User")
                        .WithMany("AccessTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
