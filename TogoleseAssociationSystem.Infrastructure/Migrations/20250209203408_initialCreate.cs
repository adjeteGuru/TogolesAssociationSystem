using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
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
                    IsEligibleToClaim = table.Column<bool>(type: "bit", nullable: false),
                    NextOfKin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextOfKinContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<int>(type: "int", nullable: false),
                    NextOfKinName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextOfKinContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClaimRemain = table.Column<int>(type: "int", nullable: false),
                    TotalClaimPerMember = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipContributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContributionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateOfContribution = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "NextOfKinContact", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("42f3241a-0d07-4ca6-86ed-605446caac6c"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, true, "Joe", new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", "07459999999", "NG5 9AQ", "wife", "07894432123", "Mr" },
                    { new Guid("55563f0d-12d3-40fa-8931-af70f83e74db"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), "John", "07459999999", "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("cfac1d51-f2de-4747-a191-441b71233d3a"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, true, "Doe", new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", "07459999999", "BR3 1AS", "sister", "07458893212", "Mr" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_MemberId",
                table: "Claims",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipContributions_MemberId",
                table: "MembershipContributions",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "MembershipContributions");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
