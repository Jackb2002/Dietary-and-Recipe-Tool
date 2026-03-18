using Newtonsoft.Json.Linq;
using System.Diagnostics;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp.Recipes
{
    /// <summary>
    /// Recipe source backed by the Spoonacular API.
    ///
    /// Requires a Spoonacular API key set in the SPOONACULAR_API_KEY environment variable.
    /// Free tier: 150 requests/day — https://spoonacular.com/food-api
    /// </summary>
    public class SpoonacularAPI : IRecipeSource
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        private const string BaseUrl = "https://api.spoonacular.com";
        private const string EnvVarName = "SPOONACULAR_API_KEY";

        /// <summary>
        /// Creates a SpoonacularAPI instance reading the key from the
        /// <c>SPOONACULAR_API_KEY</c> environment variable.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the env var is not set.</exception>
        public SpoonacularAPI() : this(
            new HttpClient { Timeout = TimeSpan.FromSeconds(15) },
            Environment.GetEnvironmentVariable(EnvVarName)
                ?? throw new InvalidOperationException(
                    $"Set the {EnvVarName} environment variable before using SpoonacularAPI.")) { }

        /// <summary>
        /// Creates a SpoonacularAPI instance with an injected client and key.
        /// Use this overload in tests.
        /// </summary>
        public SpoonacularAPI(HttpClient client, string apiKey)
        {
            _client = client;
            _apiKey = apiKey;
        }

        /// <inheritdoc/>
        public List<Recipe> SearchRecipes(string query, int count = 10)
        {
            if (string.IsNullOrWhiteSpace(query)) return [];

            string url = $"{BaseUrl}/recipes/complexSearch" +
                         $"?query={Uri.EscapeDataString(query)}" +
                         $"&number={count}" +
                         "&addRecipeNutrition=true" +
                         "&addRecipeInformation=true" +
                         $"&apiKey={_apiKey}";
            try
            {
                string json = Fetch(url);
                if (string.IsNullOrEmpty(json)) return [];

                JObject root = JObject.Parse(json);
                JArray? results = root["results"] as JArray;
                if (results == null) return [];

                return results
                    .Select(r => MapToRecipe(r))
                    .Where(r => r != null)
                    .Cast<Recipe>()
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SpoonacularAPI.SearchRecipes error: {ex.Message}");
                return [];
            }
        }

        /// <inheritdoc/>
        public Recipe? GetRecipeById(int id)
        {
            string url = $"{BaseUrl}/recipes/{id}/information" +
                         $"?includeNutrition=true" +
                         $"&apiKey={_apiKey}";
            try
            {
                string json = Fetch(url);
                if (string.IsNullOrEmpty(json)) return null;

                return MapToRecipe(JObject.Parse(json));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SpoonacularAPI.GetRecipeById error: {ex.Message}");
                return null;
            }
        }

        // ── private helpers ────────────────────────────────────────────────────

        private string Fetch(string url)
        {
            HttpResponseMessage response = _client.GetAsync(url).Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Spoonacular HTTP {(int)response.StatusCode}");
                return string.Empty;
            }
            return response.Content.ReadAsStringAsync().Result;
        }

        private static Recipe? MapToRecipe(JToken r)
        {
            if (r == null) return null;

            JArray nutrients = r["nutrition"]?["nutrients"] as JArray ?? [];

            float Nutrient(string name)
            {
                JToken? n = nutrients.FirstOrDefault(x =>
                    string.Equals(x["name"]?.ToString(), name,
                        StringComparison.OrdinalIgnoreCase));
                return n?["amount"]?.ToObject<float>() ?? 0f;
            }

            // Spoonacular reports Sodium in mg; convert to grams of salt (1g salt ≈ 2.5g sodium).
            float sodiumMg = Nutrient("Sodium");
            float saltG = sodiumMg / 1000f * 2.5f;

            string ingredients = string.Join("\n",
                (r["extendedIngredients"] as JArray ?? [])
                    .Select(i => i["original"]?.ToString() ?? string.Empty)
                    .Where(s => !string.IsNullOrEmpty(s)));

            string method = string.Join("\n",
                (r["analyzedInstructions"] as JArray ?? [])
                    .SelectMany(block => block["steps"] as JArray ?? [])
                    .Select(s => $"Step {s["number"]}: {s["step"]?.ToString()?.Trim()}"));

            return new Recipe
            {
                SpoonacularId = r["id"]?.ToObject<int>() ?? 0,
                Title       = r["title"]?.ToString() ?? string.Empty,
                Description = StripHtml(r["summary"]?.ToString() ?? string.Empty),
                CookTime    = $"{r["readyInMinutes"]?.ToObject<int>() ?? 0} mins",
                Serving     = r["servings"]?.ToObject<int>() ?? 0,
                RecipeUrls  = r["sourceUrl"]?.ToString() ?? string.Empty,
                Vegetarian  = r["vegetarian"]?.ToObject<bool>() ?? false,
                Vegan       = r["vegan"]?.ToObject<bool>() ?? false,
                GlutenFree  = r["glutenFree"]?.ToObject<bool>() ?? false,
                DairyFree   = r["dairyFree"]?.ToObject<bool>() ?? false,
                Ingredients = ingredients,
                Method      = method,
                Kcal        = Nutrient("Calories"),
                Fat         = Nutrient("Fat"),
                Saturates   = Nutrient("Saturated Fat"),
                Carbs       = Nutrient("Carbohydrates"),
                Sugars      = Nutrient("Sugar"),
                Fibre       = Nutrient("Fiber"),
                Protein     = Nutrient("Protein"),
                Salt        = saltG,
                // Difficulty and Rating are not provided by Spoonacular
                Difficulty  = string.Empty,
                Rating      = string.Empty,
                Reviews     = string.Empty,
                PrepTime    = string.Empty,
            };
        }

        private static string StripHtml(string html)
        {
            // Summaries come back with <b>, <a> tags — strip them for plain-text display
            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", string.Empty);
        }
    }
}
