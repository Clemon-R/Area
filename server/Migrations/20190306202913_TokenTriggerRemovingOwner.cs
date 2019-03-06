using Microsoft.EntityFrameworkCore.Migrations;

namespace Area.Migrations
{
    public partial class TokenTriggerRemovingOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Accounts_OwnerId",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Triggers_Accounts_OwnerId",
                table: "Triggers");

            migrationBuilder.DropIndex(
                name: "IX_Triggers_OwnerId",
                table: "Triggers");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_OwnerId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Triggers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tokens");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Triggers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Tokens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_AccountId",
                table: "Triggers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_AccountId",
                table: "Tokens",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Accounts_AccountId",
                table: "Tokens",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Triggers_Accounts_AccountId",
                table: "Triggers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Accounts_AccountId",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Triggers_Accounts_AccountId",
                table: "Triggers");

            migrationBuilder.DropIndex(
                name: "IX_Triggers_AccountId",
                table: "Triggers");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_AccountId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Triggers");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Tokens");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Triggers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_OwnerId",
                table: "Triggers",
                column: "OwnerId");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Triggers_Accounts_OwnerId",
                table: "Triggers",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
