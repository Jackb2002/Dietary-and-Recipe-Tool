namespace WinFormsInfoApp.Models
{
    public class Ingredient
    {
        //Convert all these to properties
        public int IngredientId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
        public double Calories { get; set; }
        public double Sugar { get; set; }
        public double Fiber { get; set; }
    }
}
