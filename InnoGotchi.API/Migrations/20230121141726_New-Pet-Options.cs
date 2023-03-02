using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.API.Migrations
{
    public partial class NewPetOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Bodies_BodyId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Eyes_EyesId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Farms_FarmId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Mouthes_MouthId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Noses_NoseId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_BodyId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_EyesId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_FarmId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_MouthId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_NoseId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "LastEntry",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastExit",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDrinkTime",
                table: "Pets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEatTime",
                table: "Pets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastHealthTime",
                table: "Pets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMoodTime",
                table: "Pets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "positionX",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "positionY",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDrinkTime",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "LastEatTime",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "LastHealthTime",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "LastMoodTime",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "positionX",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "positionY",
                table: "Pets");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEntry",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastExit",
                table: "Users",
                type: "datetime2",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Bodies_BodyId",
                table: "Pets",
                column: "BodyId",
                principalTable: "Bodies",
                principalColumn: "BodyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Eyes_EyesId",
                table: "Pets",
                column: "EyesId",
                principalTable: "Eyes",
                principalColumn: "EyesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Farms_FarmId",
                table: "Pets",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "FarmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Mouthes_MouthId",
                table: "Pets",
                column: "MouthId",
                principalTable: "Mouthes",
                principalColumn: "MouthId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Noses_NoseId",
                table: "Pets",
                column: "NoseId",
                principalTable: "Noses",
                principalColumn: "NoseId");
        }
    }
}
