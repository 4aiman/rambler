﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rambler.Data;

namespace Rambler.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190519174008_ConsolidatedMigrations")]
    partial class ConsolidatedMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("Rambler.Models.AccessToken", b =>
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

            modelBuilder.Entity("Rambler.Models.Channel", b =>
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

            modelBuilder.Entity("Rambler.Models.ChatMessage", b =>
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

            modelBuilder.Entity("Rambler.Models.ConfigurationSetting", b =>
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

            modelBuilder.Entity("Rambler.Models.ExternalAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiSource");

                    b.Property<string>("ReferenceId");

                    b.Property<int?>("UserId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ExternalAccount");
                });

            modelBuilder.Entity("Rambler.Models.Integration", b =>
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

            modelBuilder.Entity("Rambler.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Rambler.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<int>("FailedLogins");

                    b.Property<bool>("IsLocked");

                    b.Property<DateTime?>("LastLoginDate");

                    b.Property<bool>("MustChangePassword");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = 1, FailedLogins = 0, IsLocked = false, MustChangePassword = true, UserName = "Admin" }
                    );
                });

            modelBuilder.Entity("Rambler.Models.AccessToken", b =>
                {
                    b.HasOne("Rambler.Models.User", "User")
                        .WithMany("AccessTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Rambler.Models.ExternalAccount", b =>
                {
                    b.HasOne("Rambler.Models.User", "User")
                        .WithMany("ExternalAccounts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Rambler.Models.Role", b =>
                {
                    b.HasOne("Rambler.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}