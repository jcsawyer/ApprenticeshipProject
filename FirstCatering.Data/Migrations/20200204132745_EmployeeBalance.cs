using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstCatering.Data.Migrations
{
    public partial class EmployeeBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Employees",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Employees");
        }
    }
}
