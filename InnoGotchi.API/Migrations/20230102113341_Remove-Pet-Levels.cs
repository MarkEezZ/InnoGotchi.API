using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.API.Migrations
{
    public partial class RemovePetLevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "healthLevel",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "hungerLevel",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "moodLevel",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "thirstyLevel",
                table: "Pets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "healthLevel",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "hungerLevel",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "moodLevel",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "thirstyLevel",
                table: "Pets",
                type: "int",
                nullable: true);
        }
    }
}
