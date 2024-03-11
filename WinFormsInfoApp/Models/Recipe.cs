namespace WinFormsInfoApp.Models
{
    [Serializable]
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string WebsiteURL { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public List<Diet>? Diets { get; set; }

        public Recipe(int recipeId, string name, string description, string imageURL, string websiteURL, List<Ingredient> ingredients, List<Diet> diets)
        {
            RecipeId = recipeId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ImageURL = imageURL ?? throw new ArgumentNullException(nameof(imageURL));
            WebsiteURL = websiteURL ?? throw new ArgumentNullException(nameof(websiteURL));
            Ingredients = ingredients ?? throw new ArgumentNullException(nameof(ingredients));
            Diets = diets ?? throw new ArgumentNullException(nameof(diets));
        }
    }
}
