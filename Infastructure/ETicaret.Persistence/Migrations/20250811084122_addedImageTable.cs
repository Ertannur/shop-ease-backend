using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productTypes_Colors_ColorId",
                table: "productTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_productTypes_Products_ProductId",
                table: "productTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_productTypes_Sizes_SizeId",
                table: "productTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_productTypes_ProductTypeId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productTypes",
                table: "productTypes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "productTypes",
                newName: "ProductTypes");

            migrationBuilder.RenameIndex(
                name: "IX_productTypes_SizeId",
                table: "ProductTypes",
                newName: "IX_ProductTypes_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_productTypes_ProductId",
                table: "ProductTypes",
                newName: "IX_ProductTypes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_productTypes_ColorId",
                table: "ProductTypes",
                newName: "IX_ProductTypes_ColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_Colors_ColorId",
                table: "ProductTypes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_Products_ProductId",
                table: "ProductTypes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_Sizes_SizeId",
                table: "ProductTypes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_ProductTypes_ProductTypeId",
                table: "Stocks",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Colors_ColorId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Products_ProductId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_Sizes_SizeId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_ProductTypes_ProductTypeId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes");

            migrationBuilder.RenameTable(
                name: "ProductTypes",
                newName: "productTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypes_SizeId",
                table: "productTypes",
                newName: "IX_productTypes_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypes_ProductId",
                table: "productTypes",
                newName: "IX_productTypes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTypes_ColorId",
                table: "productTypes",
                newName: "IX_productTypes_ColorId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productTypes",
                table: "productTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_productTypes_Colors_ColorId",
                table: "productTypes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productTypes_Products_ProductId",
                table: "productTypes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productTypes_Sizes_SizeId",
                table: "productTypes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_productTypes_ProductTypeId",
                table: "Stocks",
                column: "ProductTypeId",
                principalTable: "productTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
