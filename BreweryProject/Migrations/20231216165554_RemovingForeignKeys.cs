using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryProject.Migrations
{
    public partial class RemovingForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrder_Beer_BeerId",
                table: "SaleOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Beer_BeerId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrder_BeerId",
                table: "SaleOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SaleOrder_BeerId",
                table: "SaleOrder",
                column: "BeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrder_Beer_BeerId",
                table: "SaleOrder",
                column: "BeerId",
                principalTable: "Beer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Beer_BeerId",
                table: "Stock",
                column: "BeerId",
                principalTable: "Beer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
