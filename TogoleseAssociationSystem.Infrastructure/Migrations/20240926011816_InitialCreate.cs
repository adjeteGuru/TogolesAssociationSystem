using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsChair = table.Column<bool>(type: "bit", nullable: false),
                    PhotoUrl = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    NextOfKin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipContributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContributionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateOfContribution = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAnnualContribution = table.Column<bool>(type: "bit", nullable: true),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipContributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipContributions_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsChair", "LastName", "MembershipDate", "NextOfKin", "PhotoUrl", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("15a5c552-a7a5-4b57-b925-76eae61574c2"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Local), "John", new byte[0], "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("44ac05a7-b794-4495-9eac-f28a2a4d94a4"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, false, "Joe", new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", new byte[0], "NG5 9AQ", "wife", "07894432123", "Mr" },
                    { new Guid("c5a05e8c-e870-4b6d-a7c8-a1a29d94fee6"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, false, "Doe", new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", new byte[0], "BR3 1AS", "sister", "07458893212", "Mr" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipContributions_MemberId",
                table: "MembershipContributions",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembershipContributions");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
