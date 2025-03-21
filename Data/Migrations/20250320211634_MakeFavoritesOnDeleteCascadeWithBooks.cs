using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.APIs.Migrations
{
    /// <inheritdoc />
    public partial class MakeFavoritesOnDeleteCascadeWithBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Books_bookId",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "bookId",
                table: "Favorites",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_bookId",
                table: "Favorites",
                newName: "IX_Favorites_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Books_BookId",
                table: "Favorites",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Books_BookId",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Favorites",
                newName: "bookId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_BookId",
                table: "Favorites",
                newName: "IX_Favorites_bookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Books_bookId",
                table: "Favorites",
                column: "bookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
