using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingMania.Data.Migrations
{
    public partial class RemoveManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FishSpeciesFishingSpots");

            migrationBuilder.CreateTable(
                name: "FishSpeciesFishingSpot",
                columns: table => new
                {
                    FishSpeciesFishingSpotsId = table.Column<int>(type: "int", nullable: false),
                    FishingSpotsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishSpeciesFishingSpot", x => new { x.FishSpeciesFishingSpotsId, x.FishingSpotsId });
                    table.ForeignKey(
                        name: "FK_FishSpeciesFishingSpot_FishingSpots_FishingSpotsId",
                        column: x => x.FishingSpotsId,
                        principalTable: "FishingSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FishSpeciesFishingSpot_FishSpecies_FishSpeciesFishingSpotsId",
                        column: x => x.FishSpeciesFishingSpotsId,
                        principalTable: "FishSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FishSpeciesFishingSpot_FishingSpotsId",
                table: "FishSpeciesFishingSpot",
                column: "FishingSpotsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FishSpeciesFishingSpot");

            migrationBuilder.CreateTable(
                name: "FishSpeciesFishingSpots",
                columns: table => new
                {
                    FishSpeciesId = table.Column<int>(type: "int", nullable: false),
                    FishingSpotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishSpeciesFishingSpots", x => new { x.FishSpeciesId, x.FishingSpotId });
                    table.ForeignKey(
                        name: "FK_FishSpeciesFishingSpots_FishingSpots_FishingSpotId",
                        column: x => x.FishingSpotId,
                        principalTable: "FishingSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FishSpeciesFishingSpots_FishSpecies_FishSpeciesId",
                        column: x => x.FishSpeciesId,
                        principalTable: "FishSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FishSpeciesFishingSpots_FishingSpotId",
                table: "FishSpeciesFishingSpots",
                column: "FishingSpotId");
        }
    }
}
