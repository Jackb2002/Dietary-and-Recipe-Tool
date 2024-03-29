using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;

namespace WinFormsInfoApp.Models
{
    /// <summary>
    /// Represents a diet with its name, description, positive priorities, and negative priorities.
    /// </summary>
    [JsonSerializable(typeof(Diet))]
    public class Diet
    {
        /// <summary>
        /// The name of the diet.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the diet.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The array of positive priorities for the diet.
        /// </summary>
        public string[] PriorityPositive { get; set; }

        /// <summary>
        /// The array of negative priorities for the diet.
        /// </summary>
        public string[] PriorityNegative { get; set; }

        /// <summary>
        /// List to store ranked recipes based on the diet's priorities.
        /// </summary>
        public List<Recipe>? RecipeRank;

        /// <summary>
        /// Constructor to initialize a Diet object.
        /// </summary>
        public Diet(string name, string description, string[] priorityPos, string[] priorityNeg)
        {
            Name = name;
            Description = description;
            PriorityPositive = priorityPos;
            PriorityNegative = priorityNeg;
        }

        /// <summary>
        /// Returns a string representation of the diet.
        /// </summary>
        /// <returns>A string containing the name and description of the diet.</returns>
        public override string? ToString()
        {
            return $"{Name} - {Description}";
        }

        /// <summary>
        /// Returns an array of default diets.
        /// </summary>
        /// <returns>An array of default Diet objects.</returns>
        public static Diet[] ReturnDefaultDiets()
        {
            // Define priorities for each diet
            string[] balancedPositive = { "Kcal", "Protein", "Fibre" };
            string[] balancedNegative = { "Saturates", "Sugars", "Salt" };
            // Add other diet priorities similarly

            // Create instances of the Diet class for each diet
            Diet balancedDiet = new Diet("Balanced Diet", "A diet with balanced nutritional values",
                                         balancedPositive, balancedNegative);
            // Create other diet instances similarly

            // Return the diets in an array
            return new Diet[] { balancedDiet, /* Add other diet instances */ };
        }

        /// <summary>
        /// Generates meals based on the diet's priorities.
        /// </summary>
        /// <param name="diet">The Diet object representing the chosen diet.</param>
        /// <param name="numMeals">The number of meals to generate.</param>
        /// <param name="recipes">The list of available recipes.</param>
        /// <returns>A list of Recipe objects representing the generated meals.</returns>
        public static List<Recipe> GenerateMeals(Diet diet, int numMeals, List<Recipe> recipes)
        {
            Debug.WriteLine($"Received instruction for diet {diet}, meals {numMeals} out of {recipes.Count} options");

            string[] priorities = diet.PriorityPositive;
            var recipeRanks = GenerateRankedRecipes(recipes, priorities);
            recipeRanks.OrderByDescending(x => x.Value); // Order by highest value first
            return recipeRanks.Keys.Take(numMeals).ToList(); // Return the top n recipes
        }

        /// <summary>
        /// Return an unordered list of the total values of each recipe based on the diet's priorities
        /// </summary>
        /// <param name="recipes">A list of recipes</param>
        /// <param name="priorities">A list of priorities</param>
        /// <returns>A dictionary containing recipes and their total scores.</returns>
        private static Dictionary<Recipe, float> GenerateRankedRecipes(List<Recipe> recipes, string[] priorities)
        {
            Dictionary<Recipe, float> recipeRanks = new Dictionary<Recipe, float>();
            foreach (var recipe in recipes)
            {
                float total = 0;
                foreach (var priority in priorities)
                {
                    total += GetNormalizedPropertyValue(recipe, priority);
                }
                recipeRanks.Add(recipe, total);
            }

            return recipeRanks;
        }

        /// <summary>
        /// Get the normalized value of a property for a given recipe.
        /// </summary>
        /// <param name="recipe">The recipe to calculate the property for.</param>
        /// <param name="propertyName">The name of the property to calculate.</param>
        /// <returns>The normalized value of the property.</returns>
        private static float GetNormalizedPropertyValue(Recipe recipe, string propertyName)
        {
            switch (propertyName)
            {
                case "Kcal": return recipe.Kcal / Recipe.MAX_KCAL; // Normalize to range [0, 1]
                case "Fat": return recipe.Fat / Recipe.MAX_FAT;
                case "Saturates": return recipe.Saturates / Recipe.MAX_SATURATES;
                case "Sugars": return recipe.Sugars / Recipe.MAX_SUGARS;
                case "Salt": return recipe.Salt / Recipe.MAX_SALT;
                case "Protein": return recipe.Protein / Recipe.MAX_PROTEIN;
                case "Carbs": return recipe.Carbs / Recipe.MAX_CARBS;
                case "Fibre": return recipe.Fibre / Recipe.MAX_FIBRE;

                default: return 0;
            }
        }
    }
}
