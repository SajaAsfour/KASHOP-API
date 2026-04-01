using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KASHOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class productModuleUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandTranslations_Products_ProductId",
                table: "BrandTranslations");

            migrationBuilder.DropIndex(
                name: "IX_BrandTranslations_ProductId",
                table: "BrandTranslations");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "BrandTranslations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "BrandTranslations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrandTranslations_ProductId",
                table: "BrandTranslations",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandTranslations_Products_ProductId",
                table: "BrandTranslations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
