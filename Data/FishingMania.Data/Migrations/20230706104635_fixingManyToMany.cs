using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingMania.Data.Migrations
{
    public partial class fixingManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FishSpeciesFishingSpots",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FishSpeciesFishingSpots",
                table: "FishSpeciesFishingSpots",
                columns: new[] { "Id", "FishSpeciesId", "FishingSpotId" });

            migrationBuilder.CreateIndex(
                name: "IX_FishSpeciesFishingSpots_FishSpeciesId",
                table: "FishSpeciesFishingSpots",
                column: "FishSpeciesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FishSpeciesFishingSpots",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.DropIndex(
                name: "IX_FishSpeciesFishingSpots_FishSpeciesId",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FishSpeciesFishingSpots",
                table: "FishSpeciesFishingSpots",
                columns: new[] { "FishSpeciesId", "FishingSpotId" });
        }
    }
}
