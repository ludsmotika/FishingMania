using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingMania.Data.Migrations
{
    public partial class ClearManyToManyProblems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishSpeciesFishingSpot_FishSpecies_FishSpeciesFishingSpotsId",
                table: "FishSpeciesFishingSpot");

            migrationBuilder.RenameColumn(
                name: "FishSpeciesFishingSpotsId",
                table: "FishSpeciesFishingSpot",
                newName: "FishSpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_FishSpeciesFishingSpot_FishSpecies_FishSpeciesId",
                table: "FishSpeciesFishingSpot",
                column: "FishSpeciesId",
                principalTable: "FishSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishSpeciesFishingSpot_FishSpecies_FishSpeciesId",
                table: "FishSpeciesFishingSpot");

            migrationBuilder.RenameColumn(
                name: "FishSpeciesId",
                table: "FishSpeciesFishingSpot",
                newName: "FishSpeciesFishingSpotsId");

            migrationBuilder.AddForeignKey(
                name: "FK_FishSpeciesFishingSpot_FishSpecies_FishSpeciesFishingSpotsId",
                table: "FishSpeciesFishingSpot",
                column: "FishSpeciesFishingSpotsId",
                principalTable: "FishSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
