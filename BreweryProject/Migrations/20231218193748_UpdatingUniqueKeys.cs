using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryProject.Migrations
{
    public partial class UpdatingUniqueKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_BeerId",
                table: "Stocks");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BeerId_WholesalerId",
                table: "Stocks",
                columns: new[] { "BeerId", "WholesalerId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_BeerId_WholesalerId",
                table: "Stocks");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BeerId",
                table: "Stocks",
                column: "BeerId",
                unique: true);
        }
    }
}
