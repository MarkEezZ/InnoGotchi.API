using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.API.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Noses");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Mouthes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Eyes");

            migrationBuilder.InsertData(
                table: "Bodies",
                columns: new[] { "BodyId", "FileName", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "pear_white.png", "White Pear", "pear" },
                    { 2, "oval_white.png", "White Oval", "oval" },
                    { 3, "square_white.png", "White Square", "square" },
                    { 4, "egg_white.png", "White Egg", "egg" },
                    { 5, "circle_white.png", "White Circle", "circle" },
                    { 6, "pear_black.png", "Black Pear", "pear" },
                    { 7, "oval_black.png", "Black Oval", "oval" },
                    { 8, "square_black.png", "Black Square", "square" },
                    { 9, "egg_black.png", "Black Egg", "egg" },
                    { 10, "circle_black.png", "Black Circle", "circle" }
                });

            migrationBuilder.InsertData(
                table: "Eyes",
                columns: new[] { "EyesId", "FileName", "Name" },
                values: new object[,]
                {
                    { 1, "eyes_round.png", "Round Eyes" },
                    { 2, "eyes_oblong.png", "Oblong Eyes" },
                    { 3, "eyes_vertical_oval.png", "Vertical Oval Eyes" },
                    { 4, "eyes_vertical_ellipse.png", "Vertical Ellipse Eyes" },
                    { 5, "eyes_square.png", "Square Eyes" },
                    { 6, "eyes_one.png", "One Eye" }
                });

            migrationBuilder.InsertData(
                table: "Farms",
                columns: new[] { "FarmId", "Name" },
                values: new object[,]
                {
                    { 1, "Dungeon" },
                    { 2, "AWP Lego" }
                });

            migrationBuilder.InsertData(
                table: "Mouthes",
                columns: new[] { "MouthId", "FileName", "Name" },
                values: new object[,]
                {
                    { 1, "mouth_cat.png", "Cat Mouth" },
                    { 2, "mouth_small_smile.png", "Small Smile" },
                    { 3, "mouth_monster.png", "Monster Mouth" },
                    { 4, "mouth_monster_libs.png", "Monster Libs" },
                    { 5, "mouth_vampire.png", "Vampire Mouth" },
                    { 6, "mouth_small_dash.png", "Small Dash" }
                });

            migrationBuilder.InsertData(
                table: "Noses",
                columns: new[] { "NoseId", "FileName", "Name" },
                values: new object[,]
                {
                    { 1, "nose_cat.png", "Cat Nose" },
                    { 2, "nose_round.png", "Round Nose" },
                    { 3, "nose_anime.png", "Anime Nose" },
                    { 4, "nose_egg.png", "Egg Nose" },
                    { 5, "nose_sharp.png", "Sharp Nose" }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "SettingsId", "AvatarFileName", "IsInGame", "IsMusic" },
                values: new object[,]
                {
                    { 1, "ava_1.png", false, true },
                    { 2, "ava_2.png", false, false }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "BodyId", "EyesId", "FarmId", "MouthId", "Name", "NoseId" },
                values: new object[,]
                {
                    { 1, 5, 1, 1, 2, "Dungeon Master", 5 },
                    { 2, 8, 4, 2, 3, "Dungeon Grossmeister", 4 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Age", "Email", "LastEntry", "LastExit", "Name", "Password", "SettingsId" },
                values: new object[,]
                {
                    { 1, 19, "goog55776@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarkEezZ", "qwe123", 1 },
                    { 2, 20, "goog55776x2@gmail.com", new DateTime(2022, 11, 9, 18, 22, 24, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 9, 19, 42, 34, 0, DateTimeKind.Unspecified), "John Lenon", "asd456", 2 }
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestsId", "FarmId", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnersId", "FarmId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Eyes",
                keyColumn: "EyesId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Eyes",
                keyColumn: "EyesId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Eyes",
                keyColumn: "EyesId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Eyes",
                keyColumn: "EyesId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestsId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mouthes",
                keyColumn: "MouthId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mouthes",
                keyColumn: "MouthId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Mouthes",
                keyColumn: "MouthId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Mouthes",
                keyColumn: "MouthId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Noses",
                keyColumn: "NoseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Noses",
                keyColumn: "NoseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Noses",
                keyColumn: "NoseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnersId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnersId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "PetId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "PetId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bodies",
                keyColumn: "BodyId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Eyes",
                keyColumn: "EyesId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Eyes",
                keyColumn: "EyesId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Farms",
                keyColumn: "FarmId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Farms",
                keyColumn: "FarmId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mouthes",
                keyColumn: "MouthId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mouthes",
                keyColumn: "MouthId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Noses",
                keyColumn: "NoseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Noses",
                keyColumn: "NoseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "SettingsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "SettingsId",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Noses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Mouthes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Eyes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
