using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TogoleseAssociationSystem.Application.Migrations
{
    public partial class AddMemberDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HasRoles_Roles_RoleId",
                table: "HasRoles");

            migrationBuilder.DropIndex(
                name: "IX_HasRoles_RoleId",
                table: "HasRoles");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Members");

            migrationBuilder.CreateIndex(
                name: "IX_HasRoles_RoleId",
                table: "HasRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_HasRoles_Roles_RoleId",
                table: "HasRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
