using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinFormsInfoApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIngredientClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Product_Weight",
                table: "Ingreidient",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product_Weight",
                table: "Ingreidient");
        }
    }
}
