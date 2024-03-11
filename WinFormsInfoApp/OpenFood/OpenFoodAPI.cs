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
            @"https://world.openfoodfacts.net/api/v2/";

        public IIngredientContext.ConnectionType connectionType => IIngredientContext.ConnectionType.Remote;

        private string customUserAgent = "Foodapp/Testing Nomail";

        public List<Ingredient>? GetAllIngredients(string name)
        {
            return null;
        }

        public Ingredient? GetFirstIngredient(string catagory_name)
        {
            catagory_name = catagory_name.Trim().ToLower();
            if(string.IsNullOrEmpty(catagory_name))
            {
                return null;
            }
            try
            {
                string apiUrl = $"{AccessString}search?fields=product_name,product_name_en,nutrient_levels," +
                    $"nutriments&categories_tags={catagory_name}&page_size=200&page=1&countries_tags_en=united-kingdom&states_tags=Complete";
                Debug.WriteLine($"Making request for {catagory_name} using {apiUrl}");
                return null;
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
                string requestString = AccessString + @"search?fields=product_name&search_term=chocolate";
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
