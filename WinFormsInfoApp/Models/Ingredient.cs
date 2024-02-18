
namespace WinFormsInfoApp.Models
{
    public class Ingredient
    {
        //Convert all these to properties
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
        public double Calories { get; set; }
        public double Sugar { get; set; }
        public double Fiber { get; set; }
        public double Product_Weight { get; set; }

        public Ingredient(int ingredientId, string name, string description, double fat, double carbohydrates, double protein, double calories, double sugar, double fiber, double product_Weight)
        {
            IngredientId = ingredientId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Fat = fat;
            Carbohydrates = carbohydrates;
            Protein = protein;
            Calories = calories;
            Sugar = sugar;
            Fiber = fiber;
            Product_Weight = product_Weight;
        }
    }
}
