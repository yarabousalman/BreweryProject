using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brewery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brewery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beer",
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

            migrationBuilder.CreateIndex(
                name: "IX_Brewery_Name",
                table: "Brewery",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropTable(
                name: "Brewery");
        }
    }
}
