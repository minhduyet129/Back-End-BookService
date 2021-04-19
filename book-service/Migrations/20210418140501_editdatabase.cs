using Microsoft.EntityFrameworkCore.Migrations;

namespace book_service.Migrations
{
    public partial class editdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowBooks_AspNetUsers_ApplicationUserId",
                table: "BorrowBooks");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "BorrowBooks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowBooks_ApplicationUserId",
                table: "BorrowBooks",
                newName: "IX_BorrowBooks_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowBooks_AspNetUsers_UserId",
                table: "BorrowBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowBooks_AspNetUsers_UserId",
                table: "BorrowBooks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BorrowBooks",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowBooks_UserId",
                table: "BorrowBooks",
                newName: "IX_BorrowBooks_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowBooks_AspNetUsers_ApplicationUserId",
                table: "BorrowBooks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
