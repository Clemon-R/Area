using Microsoft.EntityFrameworkCore.Migrations;

namespace Area.Migrations
{
    public partial class ListOfTriggersAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Triggers_Accounts_OwnerId",
                table: "Triggers");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Triggers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Triggers_Accounts_OwnerId",
                table: "Triggers",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Triggers_Accounts_OwnerId",
                table: "Triggers");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Triggers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Triggers_Accounts_OwnerId",
                table: "Triggers",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
