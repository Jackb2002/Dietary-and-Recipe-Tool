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
        public Dictionary<Recipe, float>? RecipeRank;

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
            string[] balancedPositive = { "Kcal", "Protein", "Fibre" };
            string[] balancedNegative = { "Saturates", "Sugars", "Salt" };

            string[] lowCarbPositive = { "Protein", "Fibre" };
            string[] lowCarbNegative = { "Carbs", "Sugars" };

            string[] lowFatPositive = { "Protein", "Fibre" };
            string[] lowFatNegative = { "Fat", "Saturates" };

            string[] highProteinPositive = { "Protein", "Fibre" };
            string[] highProteinNegative = { "Carbs", "Sugars", "Fat", "Saturates" };

            string[] lowProPositive = { "Fibre" };
            string[] lowProNegative = { "Protein", "Saturates" };

            string[] highFibrePositive = { "Fibre" };
            string[] highFibreNegative = { "Carbs" };

            // Create instances of the Diet class for each diet
            Diet balancedDiet = new Diet("Balanced Diet", "A diet with balanced nutritional values",
                                         balancedPositive, balancedNegative);

            Diet lowCarbDiet = new Diet("Low Carb Diet", "A diet low in carbohydrates",
                                         lowCarbPositive, lowCarbNegative);

            Diet lowFatDiet = new Diet("Low Fat Diet", "A diet low in fat",
                                       lowFatPositive, lowFatNegative);

            Diet highProteinDiet = new Diet("High Protein Diet", "A diet high in protein",
                                            highProteinPositive, highProteinNegative);

            Diet lowProteinDiet = new Diet("Low Protein Diet", "A diet consisting only of low prottein and saturated fats",
                                      lowProPositive, lowProNegative);

            Diet highFibreDiet = new Diet("High Fibre Diet", "A diet containing lots of fibre",
                                           highFibrePositive, highFibreNegative);

            // Return the diets in an array
            return [balancedDiet, lowCarbDiet, lowFatDiet, highProteinDiet, lowProteinDiet, highFibreDiet];
        }

        /// <summary>
        /// Generates meals based on the diet's priorities.
        /// </summary>
        /// <param name="diet">The Diet object representing the chosen diet.</param>
        /// <param name="recipes">The list of available recipes.</param>
        /// <param name="numMeals">(Optional) The number of meals to generate. -1 does all the recipes</param>
        /// <returns>A list of Recipe objects representing the generated meals.</returns>
        public static Dictionary<Recipe, float> GenerateMeals(Diet diet, List<Recipe> recipes, int numMeals = -1)
        {
            if(numMeals == -1) numMeals = recipes.Count; // Default to all recipes if not specified
            Debug.WriteLine($"Received instruction for diet {diet}, meals {numMeals} out of {recipes.Count} options");

            if (diet.RecipeRank != null) return diet.RecipeRank; // Return the cached ranking if it exists

            string[] priorities = diet.PriorityPositive;
            var recipeRanks = GenerateRankedRecipes(recipes, priorities);
            // Sort the recipes by their total scores
            var sortedRecipes = recipeRanks.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            // Generate a ranking dictionary for the recipes with recipe, rank
            return diet.RecipeRank = sortedRecipes;
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
