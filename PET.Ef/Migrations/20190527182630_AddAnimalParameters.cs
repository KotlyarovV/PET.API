using Microsoft.EntityFrameworkCore.Migrations;

namespace PET.Ef.Migrations
{
    public partial class AddAnimalParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "Animals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Passport",
                table: "Animals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sterilization",
                table: "Animals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Vaccination",
                table: "Animals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Passport",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Sterilization",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Vaccination",
                table: "Animals");
        }
    }
}
