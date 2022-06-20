using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.CodeFirst.Migrations
{
    public partial class IndexForUrlAndBarcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductUrl_Barcode",
                table: "Products",
                columns: new[] { "ProductUrl", "Barcode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductUrl_Barcode",
                table: "Products");
        }
    }
}
