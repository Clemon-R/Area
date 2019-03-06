using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Area.Migrations
{
    public partial class AccountLastVerificationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastVerificationDate",
                table: "Triggers");

            migrationBuilder.AddColumn<int>(
                name: "ActionType",
                table: "Triggers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReactionType",
                table: "Triggers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVerificationDate",
                table: "Accounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "Triggers");

            migrationBuilder.DropColumn(
                name: "ReactionType",
                table: "Triggers");

            migrationBuilder.DropColumn(
                name: "LastVerificationDate",
                table: "Accounts");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVerificationDate",
                table: "Triggers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
