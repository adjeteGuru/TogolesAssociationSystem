using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("96f6b2dd-bc82-48d4-8a4b-900b79941db3"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("e58a5765-6e09-431b-9af3-b363e56d80f7"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("f56fc7b0-772b-4c88-a9d8-97aee2ce6d9c"));

            migrationBuilder.AddColumn<string>(
                name: "NextOfKinContact",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextOfKinContact",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextOfKinName",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "NextOfKinContact", "PhotoUrl", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("58be5772-8485-4489-a0bf-e6baf4876fa1"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "John", null, new byte[0], "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("90ce893c-0418-4760-9764-d88e08626184"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, true, "Doe", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", null, new byte[0], "BR3 1AS", "sister", "07458893212", "Mr" },
                    { new Guid("fe2a3f76-ebf7-4c4f-a06f-ee367d6ea62f"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, true, "Joe", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", null, new byte[0], "NG5 9AQ", "wife", "07894432123", "Mr" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("58be5772-8485-4489-a0bf-e6baf4876fa1"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("90ce893c-0418-4760-9764-d88e08626184"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("fe2a3f76-ebf7-4c4f-a06f-ee367d6ea62f"));

            migrationBuilder.DropColumn(
                name: "NextOfKinContact",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "NextOfKinContact",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "NextOfKinName",
                table: "Claims");

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "PhotoUrl", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("96f6b2dd-bc82-48d4-8a4b-900b79941db3"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Local), "John", new byte[0], "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("e58a5765-6e09-431b-9af3-b363e56d80f7"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, true, "Joe", new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", new byte[0], "NG5 9AQ", "wife", "07894432123", "Mr" },
                    { new Guid("f56fc7b0-772b-4c88-a9d8-97aee2ce6d9c"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, true, "Doe", new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", new byte[0], "BR3 1AS", "sister", "07458893212", "Mr" }
                });
        }
    }
}
