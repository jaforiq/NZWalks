﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NZWalks.Data;

#nullable disable

namespace NZWalks.Migrations
{
    [DbContext(typeof(NZWalkAuthDbContext))]
    [Migration("20240106110919_migrationSeed")]
    partial class migrationSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NZWalks.Models.Domains.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2593f882-f2c7-4cfe-9e96-856aad7af6fe"),
                            Name = "Eassy"
                        },
                        new
                        {
                            Id = new Guid("f0167be5-5c26-4c60-8161-640031a0c998"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("ff683615-a46f-437a-8b09-ab486c1408f5"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("NZWalks.Models.Domains.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("858e6ad5-3fd0-472a-8d2d-863e0dd4a1f8"),
                            Code = "WLT",
                            ImageURL = "Ase.jpg",
                            Name = "Wellington"
                        },
                        new
                        {
                            Id = new Guid("c560da99-b186-47ed-9ae3-668c11bbc64d"),
                            Code = "BG",
                            Name = "Bogura"
                        },
                        new
                        {
                            Id = new Guid("1111b3f9-378c-40b1-a18a-0dc79546bd8b"),
                            Code = "BG",
                            ImageURL = "Ase.jpg",
                            Name = "Bogura"
                        });
                });

            modelBuilder.Entity("NZWalks.Models.Domains.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKM")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NZWalks.Models.Domains.Walk", b =>
                {
                    b.HasOne("NZWalks.Models.Domains.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NZWalks.Models.Domains.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
