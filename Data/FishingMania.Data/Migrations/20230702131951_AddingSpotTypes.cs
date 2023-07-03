#nullable disable

namespace FishingMania.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddingSpotTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FishingSpotType",
                table: "FishingSpots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FishingSpotType",
                table: "FishingSpots");
        }
    }
}
