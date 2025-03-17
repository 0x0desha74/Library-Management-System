using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.APIs.Migrations
{
    /// <inheritdoc />
    public partial class ModifyAddBorrowRecordsTableAndFinesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BorrowRecordId",
                table: "Fines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Fines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Fines_BorrowRecordId",
                table: "Fines",
                column: "BorrowRecordId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_BorrowRecords_BorrowRecordId",
                table: "Fines",
                column: "BorrowRecordId",
                principalTable: "BorrowRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_BorrowRecords_BorrowRecordId",
                table: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_Fines_BorrowRecordId",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "BorrowRecordId",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Fines");
        }
    }
}
