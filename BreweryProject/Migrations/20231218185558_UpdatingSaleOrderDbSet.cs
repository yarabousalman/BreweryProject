using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryProject.Migrations
{
    public partial class UpdatingSaleOrderDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrder_Wholesalers_WholesalerId",
                table: "SaleOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrder",
                table: "SaleOrder");

            migrationBuilder.RenameTable(
                name: "SaleOrder",
                newName: "SaleOrders");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrder_WholesalerId",
                table: "SaleOrders",
                newName: "IX_SaleOrders_WholesalerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrders",
                table: "SaleOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrders_Wholesalers_WholesalerId",
                table: "SaleOrders",
                column: "WholesalerId",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrders_Wholesalers_WholesalerId",
                table: "SaleOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrders",
                table: "SaleOrders");

            migrationBuilder.RenameTable(
                name: "SaleOrders",
                newName: "SaleOrder");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrders_WholesalerId",
                table: "SaleOrder",
                newName: "IX_SaleOrder_WholesalerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrder",
                table: "SaleOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrder_Wholesalers_WholesalerId",
                table: "SaleOrder",
                column: "WholesalerId",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
