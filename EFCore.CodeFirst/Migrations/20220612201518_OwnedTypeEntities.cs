using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.CodeFirst.Migrations
{
    public partial class OwnedTypeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OwnedType_CreatedTime",
                table: "Managers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnedType_ModifiedTime",
                table: "Managers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnedType_CreatedTime",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnedType_ModifiedTime",
                table: "Employees",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnedType_CreatedTime",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "OwnedType_ModifiedTime",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "OwnedType_CreatedTime",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OwnedType_ModifiedTime",
                table: "Employees");
        }
    }
}
