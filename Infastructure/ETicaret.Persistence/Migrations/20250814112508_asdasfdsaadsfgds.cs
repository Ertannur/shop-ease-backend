using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class asdasfdsaadsfgds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdressAppUser");

            migrationBuilder.AddColumn<Guid>(
                name: "AdressId",
                table: "Baskets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Adresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_AdressId",
                table: "Baskets",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_UserId",
                table: "Adresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AdressId",
                table: "Order",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_BasketId",
                table: "Order",
                column: "BasketId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_AspNetUsers_UserId",
                table: "Adresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Adresses_AdressId",
                table: "Baskets",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_AspNetUsers_UserId",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Adresses_AdressId",
                table: "Baskets");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_AdressId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_UserId",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "AdressId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Adresses");

            migrationBuilder.CreateTable(
                name: "AdressAppUser",
                columns: table => new
                {
                    AdressesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdressAppUser", x => new { x.AdressesId, x.AppUsersId });
                    table.ForeignKey(
                        name: "FK_AdressAppUser_Adresses_AdressesId",
                        column: x => x.AdressesId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdressAppUser_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdressAppUser_AppUsersId",
                table: "AdressAppUser",
                column: "AppUsersId");
        }
    }
}
