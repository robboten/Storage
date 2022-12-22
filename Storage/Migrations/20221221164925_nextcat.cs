using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storage.Migrations
{
    /// <inheritdoc />
    public partial class nextcat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductViewModelId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductViewModelId",
                table: "Product",
                column: "ProductViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductViewModel_ProductViewModelId",
                table: "Product",
                column: "ProductViewModelId",
                principalTable: "ProductViewModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductViewModel_ProductViewModelId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductViewModelId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductViewModelId",
                table: "Product");
        }
    }
}
