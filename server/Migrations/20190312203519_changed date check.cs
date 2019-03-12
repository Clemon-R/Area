using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Area.Migrations
{
    public partial class changeddatecheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastVerificationDate",
                table: "Accounts");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVerificationDate",
                table: "Triggers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastVerificationDate",
                table: "Triggers");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVerificationDate",
                table: "Accounts",
                nullable: true);
        }
    }
}
