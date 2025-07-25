﻿// <auto-generated />
using System;
using Messenger.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Messenger.Migrations
{
    [DbContext(typeof(MessengerContext))]
    partial class MessengerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Messenger.Domain.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Messenger.Domain.Models.GroupUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupUsers");
                });

            modelBuilder.Entity("Messenger.Domain.Models.IncomingMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("OutgoingMessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ReadAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReceivedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OutgoingMessageId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("IncomingMessages");
                });

            modelBuilder.Entity("Messenger.Domain.Models.OutgoingMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReceiverGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReceiverUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("ReceiverGroupId");

                    b.HasIndex("ReceiverUserId");

                    b.HasIndex("SenderId");

                    b.ToTable("OutgoingMessages");
                });

            modelBuilder.Entity("Messenger.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Messenger.Domain.Models.UserBlock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BlockedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BlockedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlockerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BlockedId");

                    b.HasIndex("BlockerId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserBlocks");
                });

            modelBuilder.Entity("Messenger.Domain.Models.GroupUser", b =>
                {
                    b.HasOne("Messenger.Domain.Models.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Messenger.Domain.Models.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Messenger.Domain.Models.IncomingMessage", b =>
                {
                    b.HasOne("Messenger.Domain.Models.OutgoingMessage", "OutgoingMessage")
                        .WithMany("IncomingMessages")
                        .HasForeignKey("OutgoingMessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Messenger.Domain.Models.User", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("OutgoingMessage");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("Messenger.Domain.Models.OutgoingMessage", b =>
                {
                    b.HasOne("Messenger.Domain.Models.Group", null)
                        .WithMany("Messages")
                        .HasForeignKey("GroupId");

                    b.HasOne("Messenger.Domain.Models.Group", "ReceiverGroup")
                        .WithMany()
                        .HasForeignKey("ReceiverGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Messenger.Domain.Models.User", "ReceiverUser")
                        .WithMany()
                        .HasForeignKey("ReceiverUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Messenger.Domain.Models.User", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ReceiverGroup");

                    b.Navigation("ReceiverUser");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Messenger.Domain.Models.UserBlock", b =>
                {
                    b.HasOne("Messenger.Domain.Models.User", "Blocked")
                        .WithMany()
                        .HasForeignKey("BlockedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Messenger.Domain.Models.User", "Blocker")
                        .WithMany()
                        .HasForeignKey("BlockerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Messenger.Domain.Models.User", null)
                        .WithMany("BlockedByUsers")
                        .HasForeignKey("UserId");

                    b.HasOne("Messenger.Domain.Models.User", null)
                        .WithMany("BlockedUsers")
                        .HasForeignKey("UserId1");

                    b.Navigation("Blocked");

                    b.Navigation("Blocker");
                });

            modelBuilder.Entity("Messenger.Domain.Models.Group", b =>
                {
                    b.Navigation("GroupUsers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Messenger.Domain.Models.OutgoingMessage", b =>
                {
                    b.Navigation("IncomingMessages");
                });

            modelBuilder.Entity("Messenger.Domain.Models.User", b =>
                {
                    b.Navigation("BlockedByUsers");

                    b.Navigation("BlockedUsers");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("SentMessages");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
