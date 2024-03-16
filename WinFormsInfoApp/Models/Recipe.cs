using CsvHelper.Configuration.Attributes;
using System.Text.Json.Serialization;
namespace WinFormsInfoApp.Models
{
    [JsonSerializable(typeof(Recipe))]
    public class Recipe
    {
        [Name("title")]
        public string Title { get; set; }

        [Name("difficulty")]
        public string Difficulty { get; set; }


        [Name("serves")]
        public string Serves { get; set; }

        [Name("rating")]
        public string Rating { get; set; }

        [Name("reviews")]
        public string Reviews { get; set; }

        [Name("vegetarian")]
        public bool Vegetarian { get; set; }

        [Name("vegan")]
        public bool Vegan { get; set; }

        [Name("dairy_free")]
        public bool DairyFree { get; set; }

        [Name("keto")]
        public bool Keto { get; set; }

        [Name("gluten_free")]
        public bool GlutenFree { get; set; }

        [Name("prep_time")]
        public string PrepTime { get; set; }

        [Name("cook_time")]
        public string CookTime { get; set; }

        [Name("ingredients")]
        public string Ingredients { get; set; }

        [Name("ingredient")]
        public string Ingredient { get; set; }

        [Name("recipe_urls")]
        public string RecipeUrls { get; set; }
    }

}