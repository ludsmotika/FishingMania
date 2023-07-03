#nullable disable

namespace FishingMania.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FishingSpots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(12,10)", precision: 12, scale: 10, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(12,10)", precision: 12, scale: 10, nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishingSpots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FishingSpots_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FishSpecies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishSpecies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FishSpecies_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Catches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FishWeight = table.Column<decimal>(type: "decimal(12,10)", precision: 12, scale: 10, nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    FishSpeciesId = table.Column<int>(type: "int", nullable: false),
                    FishingSpotId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catches_FishingSpots_FishingSpotId",
                        column: x => x.FishingSpotId,
                        principalTable: "FishingSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Catches_FishSpecies_FishSpeciesId",
                        column: x => x.FishSpeciesId,
                        principalTable: "FishSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Catches_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FishSpeciesFishingSpots",
                columns: table => new
                {
                    FishSpeciesId = table.Column<int>(type: "int", nullable: false),
                    FishingSpotId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "IX_Catches_FishingSpotId",
                table: "Catches",
                column: "FishingSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_Catches_FishSpeciesId",
                table: "Catches",
                column: "FishSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Catches_ImageId",
                table: "Catches",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Catches_IsDeleted",
                table: "Catches",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FishingSpots_ImageId",
                table: "FishingSpots",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_FishingSpots_IsDeleted",
                table: "FishingSpots",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FishSpecies_ImageId",
                table: "FishSpecies",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_FishSpecies_IsDeleted",
                table: "FishSpecies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FishSpeciesFishingSpots_FishingSpotId",
                table: "FishSpeciesFishingSpots",
                column: "FishingSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_FishSpeciesFishingSpots_IsDeleted",
                table: "FishSpeciesFishingSpots",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Images_IsDeleted",
                table: "Images",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catches");

            migrationBuilder.DropTable(
                name: "FishSpeciesFishingSpots");

            migrationBuilder.DropTable(
                name: "FishingSpots");

            migrationBuilder.DropTable(
                name: "FishSpecies");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_IsDeleted",
                table: "Settings",
                column: "IsDeleted");
        }
    }
}
