using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class REMOVEUSERINARCHIVE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPayments_Archive_Users_UserId",
                table: "UserPayments_Archive");

            migrationBuilder.DropIndex(
                name: "IX_UserPayments_Archive_UserId",
                table: "UserPayments_Archive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_Archive_UserId",
                table: "UserPayments_Archive",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPayments_Archive_Users_UserId",
                table: "UserPayments_Archive",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
