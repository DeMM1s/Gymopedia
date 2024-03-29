﻿// <auto-generated />
using System;
using Gymopedia.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gymopedia.Data.Migrations
{
    [DbContext(typeof(LocalDbContext))]
    [Migration("20221112184914_telegram3")]
    partial class telegram3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Gymopedia.Domain.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Gymopedia.Domain.Models.ClientToCoach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<long>("CoachId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("ClientToCoach");
                });

            modelBuilder.Entity("Gymopedia.Domain.Models.ClientToSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<long>("SessionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("ClientToSession");
                });

            modelBuilder.Entity("Gymopedia.Domain.Models.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("Gymopedia.Domain.Models.CoachWorkDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("CoachWorkDays");
                });

            modelBuilder.Entity("Gymopedia.Domain.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("CoachId")
                        .HasColumnType("bigint");

                    b.Property<int?>("CoachWorkDayId")
                        .HasColumnType("integer");

                    b.Property<int>("CoachWorkDayIdOwner")
                        .HasColumnType("integer");

                    b.Property<DateTime>("From")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Until")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("currentNumberOfClients")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CoachWorkDayId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Gymopedia.Domain.Models.Session", b =>
                {
                    b.HasOne("Gymopedia.Domain.Models.CoachWorkDay", null)
                        .WithMany("Sessions")
                        .HasForeignKey("CoachWorkDayId");
                });

            modelBuilder.Entity("Gymopedia.Domain.Models.CoachWorkDay", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
