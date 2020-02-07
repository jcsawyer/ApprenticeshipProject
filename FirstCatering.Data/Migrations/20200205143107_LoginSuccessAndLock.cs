using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstCatering.Data.Migrations
{
    public partial class LoginSuccessAndLock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DidLock",
                table: "Logins",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "Logins",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DidLock",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "Logins");
        }
    }
}
