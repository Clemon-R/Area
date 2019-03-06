using Microsoft.EntityFrameworkCore.Migrations;

namespace Area.Migrations
{
    public partial class TokenForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Accounts_OwnerId",
                table: "Tokens");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Tokens",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Accounts_OwnerId",
                table: "Tokens",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Accounts_OwnerId",
                table: "Tokens");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Tokens",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Accounts_OwnerId",
                table: "Tokens",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
