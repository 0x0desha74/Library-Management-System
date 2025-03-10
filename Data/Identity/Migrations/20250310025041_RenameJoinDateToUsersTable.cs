using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.APIs.Data.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RenameJoinDateToUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JoiDate",
                table: "AspNetUsers",
                newName: "JoinedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JoinedDate",
                table: "AspNetUsers",
                newName: "JoiDate");
        }
    }
}
