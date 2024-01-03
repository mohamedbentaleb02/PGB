using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGB.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeBookOrderToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookOrders_BookOrderId",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "BookOrderId",
                table: "Book",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_BookOrderId",
                table: "Book",
                newName: "IX_Book_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookOrders_OrderId",
                table: "Book",
                column: "OrderId",
                principalTable: "BookOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookOrders_OrderId",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Book",
                newName: "BookOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_OrderId",
                table: "Book",
                newName: "IX_Book_BookOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookOrders_BookOrderId",
                table: "Book",
                column: "BookOrderId",
                principalTable: "BookOrders",
                principalColumn: "Id");
        }
    }
}
