﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TogoleseAssociationSystem.Infrastructure.Database;

#nullable disable

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TogoleseAssociationSystem.Domain.Models.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsChair")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MembershipDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NextOfKin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c5a05e8c-e870-4b6d-a7c8-a1a29d94fee6"),
                            Address = "34 Bentley road",
                            City = "Birmingham",
                            DateOfBirth = new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "John",
                            IsActive = true,
                            IsChair = false,
                            LastName = "Doe",
                            MembershipDate = new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            NextOfKin = "Brenda",
                            PhotoUrl = new byte[0],
                            Postcode = "BR3 1AS",
                            Relationship = "sister",
                            Telephone = "07458893212",
                            Title = "Mr"
                        },
                        new
                        {
                            Id = new Guid("15a5c552-a7a5-4b57-b925-76eae61574c2"),
                            Address = "34 Bentley road",
                            City = "Birmingham",
                            DateOfBirth = new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Brenda",
                            IsActive = true,
                            IsChair = true,
                            LastName = "Love",
                            MembershipDate = new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            NextOfKin = "John",
                            PhotoUrl = new byte[0],
                            Postcode = "BR3 1AS",
                            Relationship = "brother",
                            Telephone = "07126678342",
                            Title = "Miss"
                        },
                        new
                        {
                            Id = new Guid("44ac05a7-b794-4495-9eac-f28a2a4d94a4"),
                            Address = "5 Batman garden",
                            City = "Nottingham",
                            DateOfBirth = new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Smith",
                            IsActive = true,
                            IsChair = false,
                            LastName = "Joe",
                            MembershipDate = new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            NextOfKin = "Jenny",
                            PhotoUrl = new byte[0],
                            Postcode = "NG5 9AQ",
                            Relationship = "wife",
                            Telephone = "07894432123",
                            Title = "Mr"
                        });
                });

            modelBuilder.Entity("TogoleseAssociationSystem.Domain.Models.MembershipContribution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ContributionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfContribution")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsAnnualContribution")
                        .HasColumnType("bit");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("MembershipContributions");
                });

            modelBuilder.Entity("TogoleseAssociationSystem.Domain.Models.MembershipContribution", b =>
                {
                    b.HasOne("TogoleseAssociationSystem.Domain.Models.Member", null)
                        .WithMany("Memberships")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TogoleseAssociationSystem.Domain.Models.Member", b =>
                {
                    b.Navigation("Memberships");
                });
#pragma warning restore 612, 618
        }
    }
}