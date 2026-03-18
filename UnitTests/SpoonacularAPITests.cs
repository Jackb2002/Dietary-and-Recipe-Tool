using System.Net;
using System.Net.Http;
using WinFormsInfoApp.Recipes;

namespace UnitTests
{
    [TestClass]
    public class SpoonacularAPITests
    {
        // ── fixture JSON ───────────────────────────────────────────────────────

        // Minimal complexSearch response with one recipe
        private const string SearchJson = """
            {
              "results": [{
                "id": 1,
                "title": "Chicken Tikka Masala",
                "readyInMinutes": 45,
                "servings": 4,
                "summary": "<b>Chicken</b> tikka masala is a classic dish.",
                "sourceUrl": "https://example.com/chicken-tikka",
                "vegetarian": false,
                "vegan": false,
                "glutenFree": true,
                "dairyFree": false,
                "extendedIngredients": [
                  { "original": "500g chicken breast" },
                  { "original": "2 tbsp tikka paste" }
                ],
                "analyzedInstructions": [{
                  "steps": [
                    { "number": 1, "step": "Marinate the chicken." },
                    { "number": 2, "step": "Cook in a pan for 20 mins." }
                  ]
                }],
                "nutrition": {
                  "nutrients": [
                    { "name": "Calories",      "amount": 420.0, "unit": "kcal" },
                    { "name": "Fat",           "amount": 12.0,  "unit": "g"    },
                    { "name": "Saturated Fat", "amount": 3.0,   "unit": "g"    },
                    { "name": "Carbohydrates", "amount": 35.0,  "unit": "g"    },
                    { "name": "Sugar",         "amount": 8.0,   "unit": "g"    },
                    { "name": "Fiber",         "amount": 4.0,   "unit": "g"    },
                    { "name": "Protein",       "amount": 38.0,  "unit": "g"    },
                    { "name": "Sodium",        "amount": 800.0, "unit": "mg"   }
                  ]
                }
              }],
              "totalResults": 1
            }
            """;

        private const string EmptySearchJson = """{ "results": [], "totalResults": 0 }""";
        private const string ErrorJson = """{ "status": "failure", "code": 401 }""";

        private static SpoonacularAPI MakeApi(string responseBody,
            HttpStatusCode status = HttpStatusCode.OK)
        {
            var handler = new StubHttpHandler(status, responseBody);
            return new SpoonacularAPI(new HttpClient(handler), "test-key");
        }

        // ── SearchRecipes ──────────────────────────────────────────────────────

        [TestMethod]
        public void SearchRecipes_Returns_Empty_For_Blank_Query()
        {
            var api = MakeApi(SearchJson);
            Assert.AreEqual(0, api.SearchRecipes("").Count);
            Assert.AreEqual(0, api.SearchRecipes("   ").Count);
        }

        [TestMethod]
        public void SearchRecipes_Returns_One_Recipe_From_Valid_Response()
        {
            var api = MakeApi(SearchJson);
            var results = api.SearchRecipes("chicken");
            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void SearchRecipes_Maps_Title()
        {
            var api = MakeApi(SearchJson);
            Assert.AreEqual("Chicken Tikka Masala", api.SearchRecipes("chicken")[0].Title);
        }

        [TestMethod]
        public void SearchRecipes_Maps_CookTime()
        {
            var api = MakeApi(SearchJson);
            Assert.AreEqual("45 mins", api.SearchRecipes("chicken")[0].CookTime);
        }

        [TestMethod]
        public void SearchRecipes_Maps_Servings()
        {
            var api = MakeApi(SearchJson);
            Assert.AreEqual(4, api.SearchRecipes("chicken")[0].Serving);
        }

        [TestMethod]
        public void SearchRecipes_Maps_DietaryFlags()
        {
            var api = MakeApi(SearchJson);
            var r = api.SearchRecipes("chicken")[0];
            Assert.IsFalse(r.Vegetarian);
            Assert.IsFalse(r.Vegan);
            Assert.IsTrue(r.GlutenFree);
            Assert.IsFalse(r.DairyFree);
        }

        [TestMethod]
        public void SearchRecipes_Maps_Nutrition()
        {
            var api = MakeApi(SearchJson);
            var r = api.SearchRecipes("chicken")[0];
            Assert.AreEqual(420f, r.Kcal);
            Assert.AreEqual(12f,  r.Fat);
            Assert.AreEqual(3f,   r.Saturates);
            Assert.AreEqual(35f,  r.Carbs);
            Assert.AreEqual(8f,   r.Sugars);
            Assert.AreEqual(4f,   r.Fibre);
            Assert.AreEqual(38f,  r.Protein);
        }

        [TestMethod]
        public void SearchRecipes_Converts_Sodium_To_Salt_Grams()
        {
            // 800mg sodium → 800/1000 * 2.5 = 2.0g salt
            var api = MakeApi(SearchJson);
            Assert.AreEqual(2.0f, api.SearchRecipes("chicken")[0].Salt, delta: 0.01f);
        }

        [TestMethod]
        public void SearchRecipes_Maps_Ingredients_As_Newline_Separated_List()
        {
            var api = MakeApi(SearchJson);
            string ingredients = api.SearchRecipes("chicken")[0].Ingredients;
            StringAssert.Contains(ingredients, "500g chicken breast");
            StringAssert.Contains(ingredients, "2 tbsp tikka paste");
        }

        [TestMethod]
        public void SearchRecipes_Maps_Method_Steps()
        {
            var api = MakeApi(SearchJson);
            string method = api.SearchRecipes("chicken")[0].Method;
            StringAssert.Contains(method, "Marinate the chicken.");
            StringAssert.Contains(method, "Cook in a pan for 20 mins.");
        }

        [TestMethod]
        public void SearchRecipes_Strips_Html_From_Summary()
        {
            var api = MakeApi(SearchJson);
            string desc = api.SearchRecipes("chicken")[0].Description;
            Assert.IsFalse(desc.Contains("<b>"), "Description should have HTML stripped.");
            StringAssert.Contains(desc, "Chicken");
        }

        [TestMethod]
        public void SearchRecipes_Returns_Empty_List_When_Results_Array_Is_Empty()
        {
            var api = MakeApi(EmptySearchJson);
            Assert.AreEqual(0, api.SearchRecipes("anything").Count);
        }

        [TestMethod]
        public void SearchRecipes_Returns_Empty_List_On_HTTP_Error()
        {
            var api = MakeApi(ErrorJson, HttpStatusCode.Unauthorized);
            Assert.AreEqual(0, api.SearchRecipes("chicken").Count);
        }

        // ── GetRecipeById ──────────────────────────────────────────────────────

        // Single-recipe response reuses the same shape as a search result item
        private const string SingleRecipeJson = """
            {
              "id": 42,
              "title": "Veggie Stir Fry",
              "readyInMinutes": 20,
              "servings": 2,
              "summary": "A quick and healthy stir fry.",
              "sourceUrl": "https://example.com/stir-fry",
              "vegetarian": true,
              "vegan": true,
              "glutenFree": false,
              "dairyFree": true,
              "extendedIngredients": [{ "original": "200g tofu" }],
              "analyzedInstructions": [{
                "steps": [{ "number": 1, "step": "Fry tofu until golden." }]
              }],
              "nutrition": {
                "nutrients": [
                  { "name": "Calories", "amount": 210.0, "unit": "kcal" },
                  { "name": "Protein",  "amount": 14.0,  "unit": "g"    },
                  { "name": "Sodium",   "amount": 400.0, "unit": "mg"   }
                ]
              }
            }
            """;

        [TestMethod]
        public void GetRecipeById_Returns_Mapped_Recipe()
        {
            var api = MakeApi(SingleRecipeJson);
            var r = api.GetRecipeById(42);
            Assert.IsNotNull(r);
            Assert.AreEqual("Veggie Stir Fry", r!.Title);
            Assert.AreEqual(210f, r.Kcal);
            Assert.IsTrue(r.Vegan);
        }

        [TestMethod]
        public void GetRecipeById_Returns_Null_On_HTTP_Error()
        {
            var api = MakeApi("{}", HttpStatusCode.NotFound);
            Assert.IsNull(api.GetRecipeById(99));
        }

        // ── constructor guard ──────────────────────────────────────────────────

        [TestMethod]
        public void Constructor_Throws_When_Env_Var_Not_Set()
        {
            // Temporarily clear the env var (restore afterwards)
            string? original = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");
            Environment.SetEnvironmentVariable("SPOONACULAR_API_KEY", null);
            try
            {
                Assert.ThrowsException<InvalidOperationException>(() => new SpoonacularAPI());
            }
            finally
            {
                Environment.SetEnvironmentVariable("SPOONACULAR_API_KEY", original);
            }
        }
    }

    [TestClass]
    public class SpoonacularAPIIntegrationTests
    {
        // Run with:   dotnet test --filter "TestCategory=Integration"
        // Skip in CI: dotnet test --filter "TestCategory!=Integration"
        // Requires SPOONACULAR_API_KEY environment variable.

        private static SpoonacularAPI? _api;

        [ClassInitialize]
        public static void Init(TestContext _)
        {
            string? key = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");
            if (!string.IsNullOrEmpty(key))
                _api = new SpoonacularAPI();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void SearchRecipes_Returns_Real_Results_For_Pasta()
        {
            if (_api == null) Assert.Inconclusive("SPOONACULAR_API_KEY not set.");

            var results = _api!.SearchRecipes("pasta", count: 5);

            Assert.IsTrue(results.Count > 0, "Expected at least one result for 'pasta'.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(results[0].Title),
                "First result should have a title.");
            Assert.IsTrue(results[0].Kcal > 0,
                "First result should have a positive kcal value.");
            // Note: complexSearch does not return extendedIngredients or analyzedInstructions.
            // Use GetRecipeById to retrieve full ingredient and method data.
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetRecipeById_Returns_Full_Recipe_With_Ingredients_And_Method()
        {
            if (_api == null) Assert.Inconclusive("SPOONACULAR_API_KEY not set.");

            // Recipe 654959 — "Pasta With Tuna" — verified to have ingredients + instructions
            var recipe = _api!.GetRecipeById(654959);

            Assert.IsNotNull(recipe, "Expected a recipe for id 654959.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(recipe!.Title));
            Assert.IsTrue(recipe.Serving > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(recipe.Ingredients),
                "Expected ingredients to be populated.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(recipe.Method),
                "Expected method steps to be populated.");
        }
    }
}
