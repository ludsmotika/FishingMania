using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingMania.Data.Migrations
{
    public partial class fixingManyToManyTableAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FishSpeciesFishingSpots",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.DropIndex(
                name: "IX_FishSpeciesFishingSpots_FishSpeciesId",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.DropIndex(
                name: "IX_FishSpeciesFishingSpots_IsDeleted",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FishSpeciesFishingSpots",
                table: "FishSpeciesFishingSpots",
                columns: new[] { "FishSpeciesId", "FishingSpotId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FishSpeciesFishingSpots",
                table: "FishSpeciesFishingSpots");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FishSpeciesFishingSpots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "FishSpeciesFishingSpots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "FishSpeciesFishingSpots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FishSpeciesFishingSpots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "FishSpeciesFishingSpots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FishSpeciesFishingSpots",
                table: "FishSpeciesFishingSpots",
                columns: new[] { "Id", "FishSpeciesId", "FishingSpotId" });

            migrationBuilder.CreateIndex(
                name: "IX_FishSpeciesFishingSpots_FishSpeciesId",
                table: "FishSpeciesFishingSpots",
                column: "FishSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_FishSpeciesFishingSpots_IsDeleted",
                table: "FishSpeciesFishingSpots",
                column: "IsDeleted");
        }
    }
}
