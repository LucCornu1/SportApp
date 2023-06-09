﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportApplication.Data;

#nullable disable

namespace SportApplication.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("SportApplication.Data.Models.Constraint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxParticipantCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("MaximumBirthdate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("MinimumBirthdate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Constraints");
                });

            modelBuilder.Entity("SportApplication.Data.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ConstraintId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndingDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<int>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ConstraintId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("SportApplication.Data.Models.Participation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Participations");
                });

            modelBuilder.Entity("SportApplication.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("SportApplication.Data.Models.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Ping Pong"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Tennis"
                        });
                });

            modelBuilder.Entity("SportApplication.Data.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndingDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SportId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("SportApplication.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@sportapp.com",
                            Firstname = "Super",
                            Gender = 0,
                            HashedPassword = "F2D81A260DEA8A100DD517984E53C56A7523D96942A834B9CDC249BD4E8C7AA9",
                            IsAdmin = true,
                            Lastname = "Admin"
                        });
                });

            modelBuilder.Entity("SportApplication.Data.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("SportApplication.Data.Models.Event", b =>
                {
                    b.HasOne("SportApplication.Data.Models.Constraint", "Constraint")
                        .WithMany("Events")
                        .HasForeignKey("ConstraintId");

                    b.HasOne("SportApplication.Data.Models.Tournament", "Tournament")
                        .WithMany("Events")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Constraint");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("SportApplication.Data.Models.Participation", b =>
                {
                    b.HasOne("SportApplication.Data.Models.Event", "Event")
                        .WithMany("Participations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportApplication.Data.Models.User", "User")
                        .WithMany("Participations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportApplication.Data.Models.Tournament", b =>
                {
                    b.HasOne("SportApplication.Data.Models.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("SportApplication.Data.Models.UserRole", b =>
                {
                    b.HasOne("SportApplication.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportApplication.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportApplication.Data.Models.Constraint", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("SportApplication.Data.Models.Event", b =>
                {
                    b.Navigation("Participations");
                });

            modelBuilder.Entity("SportApplication.Data.Models.Tournament", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("SportApplication.Data.Models.User", b =>
                {
                    b.Navigation("Participations");
                });
#pragma warning restore 612, 618
        }
    }
}
