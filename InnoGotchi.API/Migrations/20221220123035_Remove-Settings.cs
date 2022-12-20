using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.API.Migrations
{
    public partial class RemoveSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Settings_SettingsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Users_SettingsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SettingsId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "AvatarFileName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInGame",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMusic",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "AvatarFileName", "IsInGame", "IsMusic" },
                values: new object[] { "ava_default.png", true, true });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "AvatarFileName", "IsInGame", "IsMusic", "LastEntry", "LastExit" },
                values: new object[] { "ava_default.png", true, true, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFileName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsInGame",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsMusic",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "SettingsId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInGame = table.Column<bool>(type: "bit", nullable: false),
                    IsMusic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingsId);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "SettingsId", "AvatarFileName", "IsInGame", "IsMusic" },
                values: new object[] { 1, "ava_default.png", true, true });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "SettingsId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "LastEntry", "LastExit", "SettingsId" },
                values: new object[] { new DateTime(2022, 11, 9, 18, 22, 24, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 9, 19, 42, 34, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SettingsId",
                table: "Users",
                column: "SettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Settings_SettingsId",
                table: "Users",
                column: "SettingsId",
                principalTable: "Settings",
                principalColumn: "SettingsId");
        }
    }
}
