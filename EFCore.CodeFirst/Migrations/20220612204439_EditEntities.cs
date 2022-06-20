using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.CodeFirst.Migrations
{
    public partial class EditEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnedType_ModifiedTime",
                table: "Managers",
                newName: "ModifiedTime");

            migrationBuilder.RenameColumn(
                name: "OwnedType_CreatedTime",
                table: "Managers",
                newName: "CreatedTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastAccessDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedTime",
                table: "Managers",
                newName: "OwnedType_ModifiedTime");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Managers",
                newName: "OwnedType_CreatedTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastAccessDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
