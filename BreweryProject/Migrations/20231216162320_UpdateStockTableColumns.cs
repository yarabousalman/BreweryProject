using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryProject.Migrations
{
    public partial class UpdateStockTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_BeerId",
                table: "Stock");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_BeerId",
                table: "Stock",
                column: "BeerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_BeerId",
                table: "Stock");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_BeerId",
                table: "Stock",
                column: "BeerId");
        }
    }
}
