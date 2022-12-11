﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.API.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bodies",
                columns: table => new
                {
                    BodyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodies", x => x.BodyId);
                });

            migrationBuilder.CreateTable(
                name: "Eyes",
                columns: table => new
                {
                    EyesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eyes", x => x.EyesId);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    FarmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.FarmId);
                });

            migrationBuilder.CreateTable(
                name: "Mouthes",
                columns: table => new
                {
                    MouthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouthes", x => x.MouthId);
                });

            migrationBuilder.CreateTable(
                name: "Noses",
                columns: table => new
                {
                    NoseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noses", x => x.NoseId);
                });

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

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    thirstyLevel = table.Column<int>(type: "int", nullable: true),
                    hungerLevel = table.Column<int>(type: "int", nullable: true),
                    healthLevel = table.Column<int>(type: "int", nullable: true),
                    moodLevel = table.Column<int>(type: "int", nullable: true),
                    BodyId = table.Column<int>(type: "int", nullable: false),
                    EyesId = table.Column<int>(type: "int", nullable: false),
                    NoseId = table.Column<int>(type: "int", nullable: true),
                    MouthId = table.Column<int>(type: "int", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pets_Bodies_BodyId",
                        column: x => x.BodyId,
                        principalTable: "Bodies",
                        principalColumn: "BodyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Eyes_EyesId",
                        column: x => x.EyesId,
                        principalTable: "Eyes",
                        principalColumn: "EyesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "FarmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Mouthes_MouthId",
                        column: x => x.MouthId,
                        principalTable: "Mouthes",
                        principalColumn: "MouthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Noses_NoseId",
                        column: x => x.NoseId,
                        principalTable: "Noses",
                        principalColumn: "NoseId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    LastEntry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastExit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SettingsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Settings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "Settings",
                        principalColumn: "SettingsId");
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    GuestsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.GuestsId);
                    table.ForeignKey(
                        name: "FK_Guests_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "FarmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    OwnersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.OwnersId);
                    table.ForeignKey(
                        name: "FK_Owners_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "FarmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Owners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                values: new object[] { 1, "ava_default.png", true, true });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "Age", "BodyId", "EyesId", "FarmId", "MouthId", "Name", "NoseId", "healthLevel", "hungerLevel", "moodLevel", "thirstyLevel" },
                values: new object[,]
                {
                    { 1, 0, 5, 1, 1, 2, "Dungeon Master", 5, null, null, null, null },
                    { 2, 0, 8, 4, 2, 3, "Grossmeister", 4, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Age", "Email", "LastEntry", "LastExit", "Login", "Name", "Password", "SettingsId", "Surname" },
                values: new object[,]
                {
                    { 1, 19, "goog55776@gmail.com", null, null, "MarkEezZ", "Mark", "qwe123", 1, "Lovyagin" },
                    { 2, null, "goog55776x2@gmail.com", new DateTime(2022, 11, 9, 18, 22, 24, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 9, 19, 42, 34, 0, DateTimeKind.Unspecified), "Lenon123", "John", "asd456", 1, "Lenon" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Guests_FarmId",
                table: "Guests",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_UserId",
                table: "Guests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_FarmId",
                table: "Owners",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_BodyId",
                table: "Pets",
                column: "BodyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_EyesId",
                table: "Pets",
                column: "EyesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_FarmId",
                table: "Pets",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_MouthId",
                table: "Pets",
                column: "MouthId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_NoseId",
                table: "Pets",
                column: "NoseId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SettingsId",
                table: "Users",
                column: "SettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Bodies");

            migrationBuilder.DropTable(
                name: "Eyes");

            migrationBuilder.DropTable(
                name: "Farms");

            migrationBuilder.DropTable(
                name: "Mouthes");

            migrationBuilder.DropTable(
                name: "Noses");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
