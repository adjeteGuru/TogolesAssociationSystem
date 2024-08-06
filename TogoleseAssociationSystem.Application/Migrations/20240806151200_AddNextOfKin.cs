using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TogoleseAssociationSystem.Application.Migrations
{
    public partial class AddNextOfKin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NextOfKin",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationship",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextOfKin",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Relationship",
                table: "Members");
        }
    }
}
