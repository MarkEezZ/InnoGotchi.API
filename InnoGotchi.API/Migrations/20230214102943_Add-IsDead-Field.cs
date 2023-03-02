using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.API.Migrations
{
    public partial class AddIsDeadField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDead",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDead",
                table: "Pets");
        }
    }
}
