using System.Diagnostics;
using System.Text.Json.Serialization;

namespace WinFormsInfoApp.Models
{
    /// <summary>
    /// Represents a diet with its name, description, positive priorities, and negative priorities.
    /// </summary>
    [JsonSerializable(typeof(Diet))]
    public class Diet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] PriorityPositive { get; set; }
        public string[] PriorityNegative { get; set; }
        public DateTime StartDate { get; set; }
        public bool InUse { get; set; }
        public bool DefaultDiet { get; set; }
        public Dictionary<Recipe, float>? RecipeRank;

        public Diet(string name, string description, string[] priorityPositive, string[] priorityNegative, DateTime startDate, bool defaultDiet = false, bool inUse = false)
        {
            Name = name;
            Description = description;
            PriorityPositive = priorityPositive;
            PriorityNegative = priorityNegative;
            StartDate = startDate;
            InUse = inUse;
            DefaultDiet = defaultDiet;
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
            Diet balancedDiet = new("Balanced Diet", "A diet with balanced nutritional values",
                                         balancedPositive, balancedNegative, DateTime.Now, true);

            Diet lowCarbDiet = new("Low Carb Diet", "A diet low in carbohydrates",
                                         lowCarbPositive, lowCarbNegative, DateTime.Now, true);

            Diet lowFatDiet = new("Low Fat Diet", "A diet low in fat",
                                       lowFatPositive, lowFatNegative, DateTime.Now, true);

            Diet highProteinDiet = new("High Protein Diet", "A diet high in protein",
                                            highProteinPositive, highProteinNegative, DateTime.Now, true);

            Diet lowProteinDiet = new("Low Protein Diet", "A diet consisting only of low prottein and saturated fats",
                                      lowProPositive, lowProNegative, DateTime.Now, true);

            Diet highFibreDiet = new("High Fibre Diet", "A diet containing lots of fibre",
                                           highFibrePositive, highFibreNegative, DateTime.Now, true);

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
            if (numMeals == -1)
            {
                numMeals = recipes.Count; // Default to all recipes if not specified
            }

            Debug.WriteLine($"Received instruction for diet {diet}, meals {numMeals} out of {recipes.Count} options");


            string[] posPriorities = diet.PriorityPositive;
            string[] negPriorities = diet.PriorityNegative;
            Dictionary<Recipe, float> posRecipeRanks = GenerateRankedRecipes(recipes, posPriorities);
            Dictionary<Recipe, float> negRecipeRanks = GenerateRankedRecipes(recipes, negPriorities);
            // Combine the positive and negative priorities to form a total value
            Dictionary<Recipe, float> recipeRanks = posRecipeRanks.ToDictionary(x => x.Key, x => x.Value - negRecipeRanks[x.Key]);
            // Sort the recipes by their total scores
            Dictionary<Recipe, float> sortedRecipes = recipeRanks.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            // Shuffle the top 10% of the recipes
            IEnumerable<KeyValuePair<Recipe, float>> top10 = sortedRecipes.Take(sortedRecipes.Count / 10);
            Random random = new();
            //randomise the order of the top 10% of the recipes
            IEnumerable<KeyValuePair<Recipe, float>> shuffledTop10 = top10.OrderBy(x => random.Next()).ToDictionary(x => x.Key, x => x.Value).Take(numMeals);
            //return the dictionary of the shuffled top 10 and set to size of meal numbers
            return diet.RecipeRank = shuffledTop10.ToDictionary();
        }

        /// <summary>
        /// Return an unordered list of the total values of each recipe based on the diet's priorities
        /// </summary>
        /// <param name="recipes">A list of recipes</param>
        /// <param name="priorities">A list of priorities</param>
        /// <returns>A dictionary containing recipes and their total scores.</returns>
        private static Dictionary<Recipe, float> GenerateRankedRecipes(List<Recipe> recipes, string[] priorities)
        {
            Dictionary<Recipe, float> recipeRanks = [];
            foreach (Recipe recipe in recipes)
            {
                float total = 0;
                foreach (string priority in priorities)
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
            return propertyName switch
            {
                "Kcal" => recipe.Kcal / Recipe.MAX_KCAL,// Normalize to range [0, 1]
                "Fat" => recipe.Fat / Recipe.MAX_FAT,
                "Saturates" => recipe.Saturates / Recipe.MAX_SATURATES,
                "Sugars" => recipe.Sugars / Recipe.MAX_SUGARS,
                "Salt" => recipe.Salt / Recipe.MAX_SALT,
                "Protein" => recipe.Protein / Recipe.MAX_PROTEIN,
                "Carbs" => recipe.Carbs / Recipe.MAX_CARBS,
                "Fibre" => recipe.Fibre / Recipe.MAX_FIBRE,
                _ => 0,
            };
        }
    }
}
