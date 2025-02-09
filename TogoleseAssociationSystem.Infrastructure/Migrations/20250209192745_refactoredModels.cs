using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refactoredModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("4bc952fd-5e6a-45cc-aa96-d21b503d3993"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("9efde281-973c-4810-ae4b-9e6120f7ce31"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("e55e7bd0-7769-42eb-a266-4eb3f08b6d8d"));

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CurrentClaim",
                table: "Claims");

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "NextOfKinContact", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("8d495f2d-91c6-4007-89e3-f9757e1451dc"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, true, "Joe", new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", "07459999999", "NG5 9AQ", "wife", "07894432123", "Mr" },
                    { new Guid("a50ce162-5031-4edc-8a14-8cf9eac8d723"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, true, "Doe", new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", "07459999999", "BR3 1AS", "sister", "07458893212", "Mr" },
                    { new Guid("d3fb09f2-a22b-41cc-a1e1-aeb4bb07ae14"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), "John", "07459999999", "BR3 1AS", "brother", "07126678342", "Miss" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("8d495f2d-91c6-4007-89e3-f9757e1451dc"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("a50ce162-5031-4edc-8a14-8cf9eac8d723"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("d3fb09f2-a22b-41cc-a1e1-aeb4bb07ae14"));

            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoUrl",
                table: "Members",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentClaim",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "NextOfKinContact", "PhotoUrl", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("4bc952fd-5e6a-45cc-aa96-d21b503d3993"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Local), "John", "07459999999", new byte[0], "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("9efde281-973c-4810-ae4b-9e6120f7ce31"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, true, "Doe", new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", "07459999999", new byte[0], "BR3 1AS", "sister", "07458893212", "Mr" },
                    { new Guid("e55e7bd0-7769-42eb-a266-4eb3f08b6d8d"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, true, "Joe", new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", "07459999999", new byte[0], "NG5 9AQ", "wife", "07894432123", "Mr" }
                });
        }
    }
}
