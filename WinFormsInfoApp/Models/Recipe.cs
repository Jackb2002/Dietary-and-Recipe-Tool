using System.Text.Json.Serialization;

namespace WinFormsInfoApp.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public string Difficulty { get; set; }
        public string Rating { get; set; }
        public string Reviews { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool DairyFree { get; set; }
        public bool Keto { get; set; }
        public bool GlutenFree { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public string Ingredients { get; set; }
        public string RecipeUrls { get; set; }
        public float Kcal { get; set; }
        public float Fat { get; set; }
        public float Saturates { get; set; }
        public float Carbs { get; set; }
        public float Sugars { get; set; }
        public float Fibre { get; set; }
        public float Protein { get; set; }
        public float Salt { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public int Serving { get; set; }
    }
}
