using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TogoleseAssociationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "IsChair",
                table: "Members",
                newName: "IsEligibleToClaim");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PhotoUrl",
                table: "Members",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "IsEligibleToClaim",
                table: "Members",
                newName: "IsChair");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PhotoUrl",
                table: "Members",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "City", "DateOfBirth", "FirstName", "IsActive", "IsChair", "LastName", "MembershipDate", "NextOfKin", "PhotoUrl", "Postcode", "Relationship", "Telephone", "Title" },
                values: new object[,]
                {
                    { new Guid("a97e6485-8c21-47de-ae3d-1b39016f8291"), "34 Bentley road", "Birmingham", new DateTime(2000, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, false, "Doe", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Local), "Brenda", new byte[0], "BR3 1AS", "sister", "07458893212", "Mr" },
                    { new Guid("c7ce863b-c69c-4bb1-a4b9-11fd633c6283"), "34 Bentley road", "Birmingham", new DateTime(1980, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brenda", true, true, "Love", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Local), "John", new byte[0], "BR3 1AS", "brother", "07126678342", "Miss" },
                    { new Guid("dba8ae8f-a97e-4ff8-977f-7426e1be6bde"), "5 Batman garden", "Nottingham", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, false, "Joe", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Local), "Jenny", new byte[0], "NG5 9AQ", "wife", "07894432123", "Mr" }
                });
        }
    }
}
