﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rambler.Web.Data;

namespace Rambler.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("Rambler.Web.Models.AccessToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiSource");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<int>("UserId");

                    b.Property<string>("access_token");

                    b.Property<int>("expires_in");

                    b.Property<string>("refresh_token");

                    b.Property<string>("token_type");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccessTokens");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AccessToken");
                });

            modelBuilder.Entity("Rambler.Web.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Message");

                    b.Property<int>("PollingInterval");

                    b.Property<string>("Source");

                    b.Property<string>("SourceAuthorId");

                    b.Property<string>("SourceMessageId");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Rambler.Web.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GoogleTokenId");

                    b.HasKey("Id");

                    b.HasIndex("GoogleTokenId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Rambler.Web.Models.GoogleToken", b =>
                {
                    b.HasBaseType("Rambler.Web.Models.AccessToken");


                    b.ToTable("GoogleToken");

                    b.HasDiscriminator().HasValue("GoogleToken");
                });

            modelBuilder.Entity("Rambler.Web.Models.AccessToken", b =>
                {
                    b.HasOne("Rambler.Web.Models.User", "User")
                        .WithMany("AccessTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Rambler.Web.Models.User", b =>
                {
                    b.HasOne("Rambler.Web.Models.GoogleToken", "GoogleToken")
                        .WithMany()
                        .HasForeignKey("GoogleTokenId");
                });
#pragma warning restore 612, 618
        }
    }
}
