using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp.OpenFood
{
    internal class OpenFoodAPI : IIngredientContext
    {
        /// <summary>
        /// This class is a wrapper implementation for the OpenFoodFacts API. This 
        /// API is found at the access string and is usable 
        /// under the Open Database License (ODbL) v1.0.
        /// Docs at https://openfoodfacts.github.io/openfoodfacts-server/api/
        /// Ref at https://openfoodfacts.github.io/openfoodfacts-server/api/ref-v2/
        /// </summary>
        /// 
        public string AccessString =>
            @"https://world.openfoodfacts.net/api/";
        private string customUserAgent = "Foodapp/Testing Nomail";
        private string SearchString => AccessString + @"v2/search?q=";

        public List<Ingredient> GetAllIngredients(string name)
        {
            throw new NotImplementedException();
        }

        public Ingredient GetFirstIngredient(string name)
        {
            try
            {
                HttpResponseMessage response;
                using (HttpClient client = new HttpClient())
                {
                    string request = SearchString + name;
                    client.DefaultRequestHeaders.Add("User-Agent", customUserAgent);
                    response = client.GetAsync(request).Result;
                }
                if (response.IsSuccessStatusCode)
                {
                    string jsonRes = response.Content.ReadAsStringAsync().Result;
                    JObject json = JObject.Parse(jsonRes);

                    // Deserialize the JSON into objects
                    var products = JsonConvert.DeserializeObject<ProductBaseItem>(jsonResponse).Products;

                    // Search for the product by name
                    foreach (var product in products)
                    {
                        if (product.ProductName == productName)
                        {
                            Console.WriteLine($"Product Name: {product.ProductName}");
                            Console.WriteLine($"Weight: {product.ProductQuantity} {product.ProductQuantityUnit}");
                            break;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool TestConnection()
        {
            throw new NotImplementedException();
        }
    }
}
