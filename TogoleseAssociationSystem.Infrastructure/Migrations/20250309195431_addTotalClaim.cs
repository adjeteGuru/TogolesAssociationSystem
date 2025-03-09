using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTotalClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("42f3241a-0d07-4ca6-86ed-605446caac6c"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("55563f0d-12d3-40fa-8931-af70f83e74db"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("cfac1d51-f2de-4747-a191-441b71233d3a"));

            migrationBuilder.AddColumn<int>(
                name: "TotalClaimRemain",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "NextOfKinContact", "Postcode", "Relationship", "Telephone", "Title", "TotalClaimRemain" },
                values: new object[,]
                {
                    { new Guid("1613a729-bd6b-467c-8d37-71403cf5d1fc"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, true, "Joe", new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", "07459999999", "NG5 9AQ", "wife", "07894432123", "Mr", 2 },
                    { new Guid("61a9e4b0-55e2-4b51-90b4-df25c7199f00"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Local), "John", "07459999999", "BR3 1AS", "brother", "07126678342", "Miss", 2 },
                    { new Guid("f5147e46-9581-4b2c-a63e-9801e8e42c4f"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, true, "Doe", new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", "07459999999", "BR3 1AS", "sister", "07458893212", "Mr", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("1613a729-bd6b-467c-8d37-71403cf5d1fc"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("61a9e4b0-55e2-4b51-90b4-df25c7199f00"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("f5147e46-9581-4b2c-a63e-9801e8e42c4f"));

            migrationBuilder.DropColumn(
                name: "TotalClaimRemain",
                table: "Members");

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "NextOfKinContact", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("42f3241a-0d07-4ca6-86ed-605446caac6c"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, true, "Joe", new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", "07459999999", "NG5 9AQ", "wife", "07894432123", "Mr" },
                    { new Guid("55563f0d-12d3-40fa-8931-af70f83e74db"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), "John", "07459999999", "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("cfac1d51-f2de-4747-a191-441b71233d3a"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, true, "Doe", new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", "07459999999", "BR3 1AS", "sister", "07458893212", "Mr" }
                });
        }
    }
}
