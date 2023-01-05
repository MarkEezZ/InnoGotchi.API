using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoGotchi.API.Migrations
{
    public partial class CreateStatisticsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlivePetsCount = table.Column<int>(type: "int", nullable: false),
                    DeadPetsCount = table.Column<int>(type: "int", nullable: false),
                    AverageFeedingPeriod = table.Column<int>(type: "int", nullable: false),
                    AverageThirstPeriod = table.Column<int>(type: "int", nullable: false),
                    AverageHappinessPeriod = table.Column<int>(type: "int", nullable: false),
                    AverageAge = table.Column<int>(type: "int", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.StatisticsId);
                    table.ForeignKey(
                        name: "FK_Statistics_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "FarmId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Statistics",
                columns: new[] { "StatisticsId", "AlivePetsCount", "AverageAge", "AverageFeedingPeriod", "AverageHappinessPeriod", "AverageThirstPeriod", "DeadPetsCount", "FarmId" },
                values: new object[] { 1, 1, 0, 0, 0, 0, 0, 1 });

            migrationBuilder.InsertData(
                table: "Statistics",
                columns: new[] { "StatisticsId", "AlivePetsCount", "AverageAge", "AverageFeedingPeriod", "AverageHappinessPeriod", "AverageThirstPeriod", "DeadPetsCount", "FarmId" },
                values: new object[] { 2, 1, 0, 0, 0, 0, 0, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_FarmId",
                table: "Statistics",
                column: "FarmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
