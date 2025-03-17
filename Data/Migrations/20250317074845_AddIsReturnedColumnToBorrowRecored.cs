using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.APIs.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReturnedColumnToBorrowRecored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "BorrowRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "BorrowRecords");
        }
    }
}
