using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

        public List<Ingredient>? GetAllIngredients(string name)
        {
            return null;
        }

        public Ingredient? GetFirstIngredient(string name)
        {
            name = name.Trim().ToLower();
            if(string.IsNullOrEmpty(name))
            {
                return null;
            }
            try
            {
                HttpResponseMessage response;
                using (HttpClient client = new HttpClient())
                {
                    string request = SearchString + string.Format("fields=product_name&search_term={0}",name);
                    client.DefaultRequestHeaders.Add("User-Agent", customUserAgent);
                    response = client.GetAsync(request).Result;
                }
                if (response.IsSuccessStatusCode)
                {
                    string jsonRes = response.Content.ReadAsStringAsync().Result;

                    if (response.Content.Headers.ContentType.MediaType == "application/json") { 
                        JObject jObject = JObject.Parse(jsonRes);
                        string product = jObject["products"].First.ToString();
                        if (product == null)
                        {
                            return null;
                        }
                        string tmp_path = Directory.GetCurrentDirectory() + @"\json.txt";
                        Debug.WriteLine("Saving json test to " + tmp_path);
                        File.WriteAllText(tmp_path, product);
                        return default; //string.IsNullOrWhiteSpace(product);
                    }
                    else
                    {
                        Debug.WriteLine("Error in JSON response, not application/json type.");
                        return null;
                    }
                }
                else
                {
                    Debug.WriteLine("Error in response, not successful.");
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
            using (HttpClient client = new HttpClient())
            {
                string requestString = SearchString + @"fields=product_name&search_term=chocolate";
                client.DefaultRequestHeaders.Add("User-Agent", customUserAgent);
                HttpResponseMessage response = client.GetAsync(requestString).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*private Ingredient? ConvertProduct(ProductBaseItem product, 
            ProductMiscItem? productMiscItem, ProductNutritionItem? productNutritionItem)
        {
            try
            {
                // Convert the product into an Ingredient
                Ingredient ingredient = new Ingredient(
                    ingredientId: int.Parse(product.code),
                    name: product.product_name,
                    description: product.generic_name,
                    fat: productNutritionItem?.nutriments.fat ?? 0,
                    carbohydrates: productNutritionItem?.nutriments.carbohydrates ?? 0,
                    protein: productNutritionItem?.nutriments.proteins ?? 0,
                    calories: productNutritionItem?.nutriments.energy ?? 0,
                    sugar: productNutritionItem?.nutriments.sugars ?? 0,
                    fiber: -1, //Unimplemented as of yet in the API
                    product_Weight: double.Parse(product.product_quantity)
                    );
                return ingredient;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }*/
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
