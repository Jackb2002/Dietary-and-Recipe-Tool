namespace WinFormsInfoApp.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public string Difficulty { get; set; }
        public string Serves { get; set; }
        public string Rating { get; set; }
        public string Reviews { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool DairyFree { get; set; }
        public bool Keto { get; set; }
        public bool GlutenFree { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public List<string> Ingredients { get; set; }
        public string RecipeUrl { get; set; }
    }
}
