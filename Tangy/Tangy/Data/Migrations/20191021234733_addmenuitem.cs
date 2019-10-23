using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tangy.Data.Migrations
{
    public partial class addmenuitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menuitems",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false),
                    image = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    spiceness = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    categoryid = table.Column<int>(nullable: false),
                    subcategoryid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuitems", x => x.id);
                    table.ForeignKey(
                        name: "FK_menuitems_categories_categoryid",
                        column: x => x.categoryid,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_menuitems_categories_subcategoryid",
                        column: x => x.subcategoryid,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_menuitems_categoryid",
                table: "menuitems",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_menuitems_subcategoryid",
                table: "menuitems",
                column: "subcategoryid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menuitems");
        }
    }
}
