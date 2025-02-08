using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedAnnualContribution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("6f490c19-340f-43cf-844d-5d4f4ff70a99"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("aea093d9-d1a3-4f09-a4fa-45d1138e2162"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("cb85eacf-4d6c-4501-9dd9-6a40b1e39358"));

            migrationBuilder.DropColumn(
                name: "IsAnnualContribution",
                table: "MembershipContributions");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsAnnualContribution",
                table: "MembershipContributions",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "NextOfKinContact", "PhotoUrl", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("6f490c19-340f-43cf-844d-5d4f4ff70a99"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, true, "Joe", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", "07459999999", new byte[0], "NG5 9AQ", "wife", "07894432123", "Mr" },
                    { new Guid("aea093d9-d1a3-4f09-a4fa-45d1138e2162"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "John", "07459999999", new byte[0], "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("cb85eacf-4d6c-4501-9dd9-6a40b1e39358"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, true, "Doe", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", "07459999999", new byte[0], "BR3 1AS", "sister", "07458893212", "Mr" }
                });
        }
    }
}
