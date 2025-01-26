using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateClaimAndType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("9333379b-0cdc-414c-90e2-42a1596ec55c"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("b2f569a2-5bf9-4358-b7e8-12b607611a67"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("d35c413b-4e72-4b2b-b052-82e689a711bd"));

            migrationBuilder.AddColumn<int>(
                name: "ClaimType",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ClaimType",
                table: "Claims");

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "PhotoUrl", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("9333379b-0cdc-414c-90e2-42a1596ec55c"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, false, "Joe", new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", new byte[0], "NG5 9AQ", "wife", "07894432123", "Mr" },
                    { new Guid("b2f569a2-5bf9-4358-b7e8-12b607611a67"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, false, "Doe", new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", new byte[0], "BR3 1AS", "sister", "07458893212", "Mr" },
                    { new Guid("d35c413b-4e72-4b2b-b052-82e689a711bd"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Local), "John", new byte[0], "BR3 1AS", "brother", "07126678342", "Miss" }
                });
        }
    }
}
