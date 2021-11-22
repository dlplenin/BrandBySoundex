using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrandBySoundex.Migrations
{
    public partial class MarcaUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "Brand",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_Marca",
                table: "Brand",
                column: "Marca",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Brand_Marca",
                table: "Brand");

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
