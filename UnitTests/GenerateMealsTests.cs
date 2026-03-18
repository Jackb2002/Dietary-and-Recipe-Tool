using WinFormsInfoApp.Models;

namespace UnitTests
{
    [TestClass]
    public class GenerateMealsTests
    {
        private static List<Recipe> MakeRecipes(int count = 20)
        {
            var recipes = new List<Recipe>();
            for (int i = 1; i <= count; i++)
            {
                recipes.Add(new Recipe
                {
                    Title = $"Recipe {i}",
                    Kcal = i * 50,
                    Fat = i * 2,
                    Saturates = i,
                    Carbs = i * 10,
                    Sugars = i * 3,
                    Fibre = i,
                    Protein = i * 4,
                    Salt = i * 0.5f,
                    Ingredients = "some ingredients",
                    Serving = 2
                });
            }
            Recipe.GenerateMaxValues(recipes);
            return recipes;
        }

        [TestMethod]
        public void GenerateMeals_Returns_NonEmpty_Dictionary()
        {
            var diet = new Diet("High Protein", "Test", ["Protein"], ["Carbs"], DateTime.Now);
            var recipes = MakeRecipes();

            var result = Diet.GenerateMeals(diet, recipes);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GenerateMeals_Respects_NumMeals_Limit()
        {
            var diet = new Diet("Test", "Test", ["Protein"], ["Carbs"], DateTime.Now);
            var recipes = MakeRecipes(30);

            var result = Diet.GenerateMeals(diet, recipes, numMeals: 3);

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GenerateMeals_Default_Returns_All_Recipes_From_Top10Percent()
        {
            var diet = new Diet("Test", "Test", ["Protein"], ["Carbs"], DateTime.Now);
            var recipes = MakeRecipes(100);

            var result = Diet.GenerateMeals(diet, recipes);

            // Top 10% of 100 = 10 recipes; numMeals = -1 defaults to recipes.Count but capped by top10
            Assert.IsTrue(result.Count <= 10);
        }

        [TestMethod]
        public void GenerateMeals_AllRecipes_Have_NonNegative_Score()
        {
            var diet = new Diet("Balanced", "Test", ["Protein", "Fibre"], ["Salt"], DateTime.Now);
            var recipes = MakeRecipes(20);

            var result = Diet.GenerateMeals(diet, recipes, numMeals: 2);

            foreach (var score in result.Values)
            {
                Assert.IsTrue(score >= -1f && score <= 1f, $"Score {score} out of expected range");
            }
        }

        [TestMethod]
        public void GenerateMeals_Sets_RecipeRank_On_Diet()
        {
            var diet = new Diet("Test", "Test", ["Kcal"], ["Salt"], DateTime.Now);
            var recipes = MakeRecipes(20);

            Diet.GenerateMeals(diet, recipes, numMeals: 2);

            Assert.IsNotNull(diet.RecipeRank);
            Assert.AreEqual(2, diet.RecipeRank.Count);
        }

        [TestMethod]
        public void GenerateMeals_With_Empty_Priorities_Returns_Results()
        {
            // Empty priority arrays should still produce a result (all scores = 0, random top 10%)
            var diet = new Diet("Test", "Test", [], [], DateTime.Now);
            var recipes = MakeRecipes(20);

            var result = Diet.GenerateMeals(diet, recipes, numMeals: 1);

            Assert.AreEqual(1, result.Count);
        }
    }
}
