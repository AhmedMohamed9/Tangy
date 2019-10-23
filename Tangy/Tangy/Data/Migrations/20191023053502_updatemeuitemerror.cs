using Microsoft.EntityFrameworkCore.Migrations;

namespace Tangy.Data.Migrations
{
    public partial class updatemeuitemerror : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menuitems_categories_subcategoryid",
                table: "menuitems");

            migrationBuilder.AddForeignKey(
                name: "FK_menuitems_subCategories_subcategoryid",
                table: "menuitems",
                column: "subcategoryid",
                principalTable: "subCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menuitems_subCategories_subcategoryid",
                table: "menuitems");

            migrationBuilder.AddForeignKey(
                name: "FK_menuitems_categories_subcategoryid",
                table: "menuitems",
                column: "subcategoryid",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
