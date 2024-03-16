using Newtonsoft.Json.Linq;
using System.Diagnostics;
using WinFormsInfoApp.Models;
using static WinFormsInfoApp.IIngredientContext;

namespace WinFormsInfoApp.OpenFood
{
    /// <summary>
    /// Implementation of IIngredientContext using the OpenFoodFacts API.
    /// </summary>
    internal class OpenFoodAPI : IIngredientContext
    {
        /// <summary>
        /// Access string for the OpenFoodFacts API.
        /// </summary>
        public string AccessString => @"https://world.openfoodfacts.org/api/v2/";

        /// <summary>
        /// Type of connection used by this context.
        /// </summary>
        public ConnectionType connectionType => ConnectionType.Remote;

        /// <summary>
        /// Custom user agent for HTTP requests.
        /// </summary>
        private readonly string customUserAgent = "Foodapp/Testing Nomail";

        /// <inheritdoc/>
        public List<Ingredient>? GetAllIngredients(string name)
        {
            return null;
        }

        /// <inheritdoc/>
        public Ingredient? GetFirstIngredient(string categoryName)
        {
            categoryName = categoryName.Trim().ToLower();
            if (string.IsNullOrEmpty(categoryName))
            {
                return null;
            }

            try
            {
                string apiUrl = $"{AccessString}search?fields=code,product_name,product_name_en,nutrient_levels," +
                    $"nutriments,product_quantity&categories_tags={categoryName}&page_size=200&page=1&countries_tags_en=united-kingdom&states_tags=Complete&";
                Debug.WriteLine($"Making request for {categoryName} using {apiUrl}");

                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("User-Agent", customUserAgent);
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Failed to get JSON from request");
                    return null;
                }

                JObject responseJson = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Debug.WriteLine("Successfully got JSON from request");

                if (responseJson["products"] is JArray products && products.Count > 0)
                {
                    // Parse the first product and return the ingredient
                    return ParseProduct(products[0]);
                }
                else
                {
                    Debug.WriteLine("No products found in the response");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Parses a JSON object representing a product into an Ingredient object.
        /// </summary>
        /// <param name="product">JSON object representing a product.</param>
        /// <returns>An Ingredient object parsed from the JSON data.</returns>
        private Ingredient ParseProduct(JToken product)
        {
            string name = (string)product["product_name"];
            double fat = (double)product["nutriments"]["fat_100g"];
            double carbohydrates = (double)product["nutriments"]["carbohydrates_100g"];
            double protein = (double)product["nutriments"]["proteins_100g"];
            double calories = (double)product["nutriments"]["energy-kcal_100g"];
            double sugar = (double)product["nutriments"]["sugars_100g"];
            double fiber = (double)product["nutriments"]["fiber_100g"];
            double productWeight = (double)product["product_quantity"];
            string code = (string)product["code"];
            return new Ingredient(code, name, "", fat, carbohydrates, protein, calories, sugar, fiber, productWeight);
        }

        /// <inheritdoc/>
        public bool TestConnection()
        {
            using HttpClient client = new();
            string requestString = AccessString + @"search?fields=product_name&search_term=chocolate";
            client.DefaultRequestHeaders.Add("User-Agent", customUserAgent);
            try
            {
                HttpResponseMessage response = client.GetAsync(requestString).Result;
                bool statusCode = response.IsSuccessStatusCode;
                if (statusCode)
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine($"Connection made but bad code recieved {response.StatusCode}");
                    return false;
                }
            }
            catch (AggregateException)
            {
                Debug.WriteLine("Likely failed due to no internet connection");
                return false;
            }
        }
    }
}

/*
 * Ingredient class fields
 *      public string Name { get; set; }
        public string Description { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
        public double Calories { get; set; }
        public double Sugar { get; set; }
        public double Fiber { get; set; }
        public double Product_Weight { get; set; }
 */
