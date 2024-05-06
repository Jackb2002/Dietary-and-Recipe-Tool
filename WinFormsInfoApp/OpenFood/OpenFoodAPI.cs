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

        public string BarcodeEndpoint => @"product/";

        /// <summary>
        /// Custom user agent for HTTP requests.
        /// </summary>
        private readonly string customUserAgent = "Foodapp/Testing Nomail";

        /// <summary>
        /// Gets all ingredients from the OpenFoodFacts API.
        /// </summary>
        /// <param name="ingredients">List of ingredient names you want to look up</param>
        /// <returns>C# List object of Ingredient objects containing OpenFood Information</returns>
        public List<Ingredient?[]> GetIngredientList(string[] ingredients, string location = "All")
        {
            List<Ingredient?[]> Ingredients = [];
            foreach (string item in ingredients)
            {
                Ingredients.Add(GetIngredientsByName(item, location));
            }
            return Ingredients;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public Ingredient?[]? GetIngredientsByName(string categoryName, string location = "All")
        {
            categoryName = categoryName.Trim().ToLower();
            if (string.IsNullOrEmpty(categoryName))
            {
                return null;
            }

            try
            {
                string apiUrl = location == "All"
                    ? $"{AccessString}search?fields=code,product_name,product_name_en,nutrient_levels," +
    $"nutriments,product_quantity&categories_tags={categoryName}&page_size=200&page=1&countries_tags_en=united-kingdom&"
                    : $"{AccessString}search?fields=code,product_name,product_name_en,nutrient_levels," +
    $"nutriments,product_quantity&categories_tags={categoryName}&stores_tags={location}&page_size=200&page=1&countries_tags_en=united-kingdom";
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
                    // Parse the products and return the ingredient
                    Ingredient?[] returnList = new Ingredient?[products.Count];
                    for (int i = 0; i < products.Count; i++)
                    {
                        returnList[i] = ParseProduct(products[i]);
                    }
                    return returnList;
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
        /// Return a single ingredient from the OpenFoodFacts API using a barcode.
        /// </summary>
        /// <param name="code">Barcode returned by the initial search</param>
        /// <returns>Ingredient object</returns>
        public Ingredient? GetIngredientByCode(int code)
        {
            string apiUrl = $"{AccessString}{BarcodeEndpoint}{code}";

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

        /// <summary>
        /// Parses a JSON object representing a product into an Ingredient object.
        /// </summary>
        /// <param name="product">JSON object representing a product.</param>
        /// <returns>An Ingredient object parsed from the JSON data.</returns>
        private Ingredient ParseProduct(JToken product)
        {
            string name = (string)product["product_name"] ?? "Unknown";
            double fat = product["nutriments"]["fat_100g"]?.ToObject<double>() ?? 0.0;
            double carbohydrates = product["nutriments"]["carbohydrates_100g"]?.ToObject<double>() ?? 0.0;
            double protein = product["nutriments"]["proteins_100g"]?.ToObject<double>() ?? 0.0;
            double calories = product["nutriments"]["energy-kcal_100g"]?.ToObject<double>() ?? 0.0;
            double sugar = product["nutriments"]["sugars_100g"]?.ToObject<double>() ?? 0.0;
            double fibre = product["nutriments"]["fibre_100g"]?.ToObject<double>() ?? 0.0;
            double productWeight = product["product_quantity"]?.ToObject<double>() ?? 0.0;
            string code = (string)product["code"] ?? "";

            return new Ingredient(code, name, "", fat, carbohydrates, protein, calories, sugar, fibre, productWeight);
        }


        /// <inheritdoc/>
        public bool TestConnection()
        {
            using HttpClient client = new();
            bool test_success = TestApiConnection(client, AccessString);
            return test_success;
        }

        private bool TestApiConnection(HttpClient client, string accessUrl)
        {
            string requestString = accessUrl + @"search?fields=product_name&search_term=chocolate";
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
        public double Fibre { get; set; }
        public double Product_Weight { get; set; }
 */
