using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_ProductId",
                table: "ProductTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductId_ColorId_Size",
                table: "ProductTypes",
                columns: new[] { "ProductId", "ColorId", "Size" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_ProductId_ColorId_Size",
                table: "ProductTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductId",
                table: "ProductTypes",
                column: "ProductId");
        }
    }
}
