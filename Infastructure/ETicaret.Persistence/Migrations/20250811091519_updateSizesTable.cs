using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateSizesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeType",
                table: "Sizes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sizes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sizes");

            migrationBuilder.AddColumn<int>(
                name: "SizeType",
                table: "Sizes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
