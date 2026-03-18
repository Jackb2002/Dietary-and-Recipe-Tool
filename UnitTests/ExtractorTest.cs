using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeExtractor;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ExtractorTest
    {
        // Satay sweet potato curry — a stable BBC Good Food recipe used as a fixture.
        private const string TestUrl = "https://www.bbcgoodfood.com/recipes/satay-sweet-potato-curry";

        private static List<KeyValuePair<string, object>>? _recipeData;

        [ClassInitialize]
        public static void FetchOnce(TestContext _)
        {
            // Fetch the page once and share across all tests in this class.
            _recipeData = GoodFood.ParseRecipeFromUrl(TestUrl);
        }

        private static object? Field(string key) =>
            _recipeData?.Find(kv => kv.Key == key).Value;

        [TestMethod]
        public void ParseRecipeFromUrl_Returns_NonNull_Result()
        {
            Assert.IsNotNull(_recipeData, "ParseRecipeFromUrl returned null.");
        }

        [TestMethod]
        public void ParseRecipeFromUrl_Contains_All_Expected_Fields()
        {
            string[] expectedKeys =
            [
                "Name", "Description", "Rating", "CookTime", "Difficulty",
                "Kcal", "Fat", "Saturates", "Carbs", "Sugars", "Fibre", "Protein", "Salt",
                "Ingredients", "Method", "Serving", "Url"
            ];
            foreach (string key in expectedKeys)
                Assert.IsTrue(
                    _recipeData!.Exists(kv => kv.Key == key),
                    $"Missing expected field: {key}");
        }

        [TestMethod]
        public void ParseRecipeFromUrl_Name_Is_NonEmpty()
        {
            string? name = Field("Name")?.ToString();
            Assert.IsFalse(string.IsNullOrWhiteSpace(name), "Recipe name should not be empty.");
        }

        [TestMethod]
        public void ParseRecipeFromUrl_Ingredients_Are_Listed()
        {
            string? ingredients = Field("Ingredients")?.ToString();
            Assert.IsFalse(string.IsNullOrWhiteSpace(ingredients),
                "Ingredients list should not be empty.");
        }

        [TestMethod]
        public void ParseRecipeFromUrl_Has_Positive_Kcal()
        {
            object? raw = Field("Kcal");
            Assert.IsNotNull(raw, "Kcal field should be present.");
            bool parsed = float.TryParse(raw.ToString(), out float kcal);
            Assert.IsTrue(parsed && kcal > 0,
                $"Kcal should be a positive number, got: {raw}");
        }

        [TestMethod]
        public void ParseRecipeFromUrl_Has_Positive_Serving_Count()
        {
            object? raw = Field("Serving");
            Assert.IsNotNull(raw, "Serving field should be present.");
            bool parsed = int.TryParse(raw.ToString(), out int serving);
            Assert.IsTrue(parsed && serving > 0,
                $"Serving count should be a positive integer, got: {raw}");
        }

        [TestMethod]
        public void ParseRecipeFromUrl_Url_Matches_Input()
        {
            string? returnedUrl = Field("Url")?.ToString();
            Assert.AreEqual(TestUrl, returnedUrl,
                "Returned URL should match the input URL.");
        }
    }
}
