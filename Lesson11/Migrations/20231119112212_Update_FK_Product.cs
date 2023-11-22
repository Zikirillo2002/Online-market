using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lesson11.Migrations
{
    /// <inheritdoc />
    public partial class Update_FK_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_Product_InventoryItem",
                table: "InventoryItem");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItem_InventoryItem",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "InventoryItem",
                table: "InventoryItem");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_ProductId",
                table: "InventoryItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_Product_ProductId",
                table: "InventoryItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_Product_ProductId",
                table: "InventoryItem");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItem_ProductId",
                table: "InventoryItem");

            migrationBuilder.AddColumn<int>(
                name: "InventoryItem",
                table: "InventoryItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_InventoryItem",
                table: "InventoryItem",
                column: "InventoryItem");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_Product_InventoryItem",
                table: "InventoryItem",
                column: "InventoryItem",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
