using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinFormsInfoApp.Migrations
{
    /// <inheritdoc />
    public partial class TableRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingreidient_IngredientsIngredientId",
                table: "RecipeIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingreidient",
                table: "Ingreidient");

            migrationBuilder.RenameTable(
                name: "Ingreidient",
                newName: "Ingredient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredient_IngredientsIngredientId",
                table: "RecipeIngredients",
                column: "IngredientsIngredientId",
                principalTable: "Ingredient",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredient_IngredientsIngredientId",
                table: "RecipeIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingreidient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingreidient",
                table: "Ingreidient",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingreidient_IngredientsIngredientId",
                table: "RecipeIngredients",
                column: "IngredientsIngredientId",
                principalTable: "Ingreidient",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
