using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseSolidarity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ClaimRemain",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "TotalClaimPerMember",
                table: "Claims");

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsEligibleToClaim", "LastName", "MembershipDate", "NextOfKin", "NextOfKinContact", "Postcode", "Relationship", "Telephone", "Title", "TotalClaimRemain" },
                values: new object[,]
                {
                    { new Guid("6e4ba595-10df-4bf7-9b01-6a2c3412c960"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, true, "Joe", new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", "07459999999", "NG5 9AQ", "wife", "07894432123", "Mr", 2 },
                    { new Guid("cdedad9a-30b1-461a-87b2-8dbb30db6175"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, true, "Doe", new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", "07459999999", "BR3 1AS", "sister", "07458893212", "Mr", 2 },
                    { new Guid("fe7e9f0b-bfc9-4891-b123-65adde34880f"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Local), "John", "07459999999", "BR3 1AS", "brother", "07126678342", "Miss", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("6e4ba595-10df-4bf7-9b01-6a2c3412c960"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("cdedad9a-30b1-461a-87b2-8dbb30db6175"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("fe7e9f0b-bfc9-4891-b123-65adde34880f"));

            migrationBuilder.AddColumn<int>(
                name: "ClaimRemain",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalClaimPerMember",
                table: "Claims",
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
    }
}
