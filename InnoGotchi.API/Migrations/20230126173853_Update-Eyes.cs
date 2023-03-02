using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.API.Migrations
{
    public partial class UpdateEyes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Eyes",
                columns: new[] { "EyesId", "FileName", "Name" },
                values: new object[] { 7, "eyes_tired_oval.png", "Tired Oval Eyes" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Eyes",
                keyColumn: "EyesId",
                keyValue: 7);
        }
    }
}
