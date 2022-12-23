using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storage.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDbOPVM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductViewModel_ProductViewModelId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductViewModelId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductViewModelId",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductViewModelId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductViewModel", x => x.Id);
                });

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
    }
}
