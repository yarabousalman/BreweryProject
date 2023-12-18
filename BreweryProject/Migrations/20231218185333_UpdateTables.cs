using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryProject.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrder_Wholesaler_WholesalerId",
                table: "SaleOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Wholesaler_WholesalerId",
                table: "Stock");

            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wholesaler",
                table: "Wholesaler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brewery",
                table: "Brewery");

            migrationBuilder.RenameTable(
                name: "Wholesaler",
                newName: "Wholesalers");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameTable(
                name: "Brewery",
                newName: "Breweries");

            migrationBuilder.RenameIndex(
                name: "IX_Wholesaler_Name",
                table: "Wholesalers",
                newName: "IX_Wholesalers_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_WholesalerId",
                table: "Stocks",
                newName: "IX_Stocks_WholesalerId");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_BeerId",
                table: "Stocks",
                newName: "IX_Stocks_BeerId");

            migrationBuilder.RenameIndex(
                name: "IX_Brewery_Name",
                table: "Breweries",
                newName: "IX_Breweries_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wholesalers",
                table: "Wholesalers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Breweries",
                table: "Breweries",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BreweryId = table.Column<int>(type: "int", nullable: false),
                    AlcoholContent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beers_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Breweries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BreweryId",
                table: "Beers",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_Beers_Name",
                table: "Beers",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrder_Wholesalers_WholesalerId",
                table: "SaleOrder",
                column: "WholesalerId",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Wholesalers_WholesalerId",
                table: "Stocks",
                column: "WholesalerId",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrder_Wholesalers_WholesalerId",
                table: "SaleOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Wholesalers_WholesalerId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wholesalers",
                table: "Wholesalers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Breweries",
                table: "Breweries");

            migrationBuilder.RenameTable(
                name: "Wholesalers",
                newName: "Wholesaler");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "Breweries",
                newName: "Brewery");

            migrationBuilder.RenameIndex(
                name: "IX_Wholesalers_Name",
                table: "Wholesaler",
                newName: "IX_Wholesaler_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_WholesalerId",
                table: "Stock",
                newName: "IX_Stock_WholesalerId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_BeerId",
                table: "Stock",
                newName: "IX_Stock_BeerId");

            migrationBuilder.RenameIndex(
                name: "IX_Breweries_Name",
                table: "Brewery",
                newName: "IX_Brewery_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wholesaler",
                table: "Wholesaler",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brewery",
                table: "Brewery",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Beer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlcoholContent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BreweryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beer_Brewery_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Brewery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BreweryId",
                table: "Beer",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_Name",
                table: "Beer",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrder_Wholesaler_WholesalerId",
                table: "SaleOrder",
                column: "WholesalerId",
                principalTable: "Wholesaler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Wholesaler_WholesalerId",
                table: "Stock",
                column: "WholesalerId",
                principalTable: "Wholesaler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
