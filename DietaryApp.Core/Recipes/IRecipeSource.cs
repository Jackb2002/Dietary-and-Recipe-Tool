using WinFormsInfoApp.Models;

namespace WinFormsInfoApp.Recipes
{
    /// <summary>
    /// Abstraction over any recipe data provider (Spoonacular, local cache, etc.).
    /// </summary>
    public interface IRecipeSource
    {
        /// <summary>
        /// Search for recipes matching a query term and return up to <paramref name="count"/> results.
        /// </summary>
        List<Recipe> SearchRecipes(string query, int count = 10);

        /// <summary>
        /// Fetch a single recipe by its provider-specific ID. Returns null if not found.
        /// </summary>
        Recipe? GetRecipeById(int id);
    }
}
