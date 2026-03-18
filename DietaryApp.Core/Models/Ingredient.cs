using System.Text.Json.Serialization;

namespace WinFormsInfoApp.Models
{
    [JsonSerializable(typeof(Ingredient))]
    public class Ingredient
    {
        //All nutritional values are per 100g of the ingredient and the product weight is in weither grams or ML depending on the product
        public string IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
        public double Calories { get; set; }
        public double Sugar { get; set; }
        public double Fibre { get; set; }
        public double Product_Weight { get; set; }

        public Ingredient(string ingredientId, string name, string description, double fat, double carbohydrates, double protein, double calories, double sugar, double fibre, double product_Weight)
        {
            IngredientId = ingredientId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Fat = fat;
            Carbohydrates = carbohydrates;
            Protein = protein;
            Calories = calories;
            Sugar = sugar;
            Fibre = fibre;
            Product_Weight = product_Weight;
        }
    }
}
