using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addclaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("15a5c552-a7a5-4b57-b925-76eae61574c2"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("44ac05a7-b794-4495-9eac-f28a2a4d94a4"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("c5a05e8c-e870-4b6d-a7c8-a1a29d94fee6"));

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalClaimPerMember = table.Column<int>(type: "int", nullable: false),
                    CurrentClaim = table.Column<int>(type: "int", nullable: false),
                    ClaimRemain = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsChair", "LastName", "MembershipDate", "NextOfKin", "PhotoUrl", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("a97e6485-8c21-47de-ae3d-1b39016f8291"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, false, "Doe", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", new byte[0], "BR3 1AS", "sister", "07458893212", "Mr" },
                    { new Guid("c7ce863b-c69c-4bb1-a4b9-11fd633c6283"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Local), "John", new byte[0], "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("dba8ae8f-a97e-4ff8-977f-7426e1be6bde"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, false, "Joe", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", new byte[0], "NG5 9AQ", "wife", "07894432123", "Mr" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_MemberId",
                table: "Claims",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("a97e6485-8c21-47de-ae3d-1b39016f8291"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("c7ce863b-c69c-4bb1-a4b9-11fd633c6283"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("dba8ae8f-a97e-4ff8-977f-7426e1be6bde"));

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsChair", "LastName", "MembershipDate", "NextOfKin", "PhotoUrl", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("15a5c552-a7a5-4b57-b925-76eae61574c2"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Local), "John", new byte[0], "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("44ac05a7-b794-4495-9eac-f28a2a4d94a4"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, false, "Joe", new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", new byte[0], "NG5 9AQ", "wife", "07894432123", "Mr" },
                    { new Guid("c5a05e8c-e870-4b6d-a7c8-a1a29d94fee6"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, false, "Doe", new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", new byte[0], "BR3 1AS", "sister", "07458893212", "Mr" }
                });
        }
    }
}
