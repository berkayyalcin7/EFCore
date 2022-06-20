using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.CodeFirst.Migrations
{
    public partial class TPTypeInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Persons_Id",
                        column: x => x.Id,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Persons_Id",
                        column: x => x.Id,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Persons",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }
    }
}
