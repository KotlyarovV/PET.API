using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PET.Ef.Migrations
{
    public partial class Fixforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Users_Id",
                table: "Animals");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Animals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_OwnerId",
                table: "Animals",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Users_OwnerId",
                table: "Animals",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Users_OwnerId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_OwnerId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Animals");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Users_Id",
                table: "Animals",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
