using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingMania.Data.Migrations
{
    public partial class ChangingDecimalPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(15,10)",
                precision: 15,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,10)",
                oldPrecision: 12,
                oldScale: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(12,10)",
                precision: 12,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,10)",
                oldPrecision: 15,
                oldScale: 10);
        }
    }
}
