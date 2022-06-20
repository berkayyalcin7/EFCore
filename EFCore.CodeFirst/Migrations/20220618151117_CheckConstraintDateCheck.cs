using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.CodeFirst.Migrations
{
    public partial class CheckConstraintDateCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name")
                .Annotation("SqlServer:Include", new[] { "UnitPrice", "Stock" });

            migrationBuilder.AddCheckConstraint(
                name: "DateCheck",
                table: "Products",
                sql: "[CreatedDate]>[LastAccessDate]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropCheckConstraint(
                name: "DateCheck",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");
        }
    }
}
