using Microsoft.EntityFrameworkCore.Migrations;

namespace Area.Migrations
{
    public partial class tokenAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Tokens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_OwnerId",
                table: "Tokens",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Accounts_OwnerId",
                table: "Tokens",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Accounts_OwnerId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_OwnerId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tokens");
        }
    }
}
