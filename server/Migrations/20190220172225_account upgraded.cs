using Microsoft.EntityFrameworkCore.Migrations;

namespace Area.Migrations
{
    public partial class accountupgraded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Accounts",
                nullable: true);
            migrationBuilder.InsertData(
                "Accounts",
                new[] { "Id", "UserName", "Password"},
                new[] {"1", "Admin", "Admin" }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Accounts");
        }
    }
}
