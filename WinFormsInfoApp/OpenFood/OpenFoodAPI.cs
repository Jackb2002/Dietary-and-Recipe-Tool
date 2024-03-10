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
            "https://world.openfoodfacts.net/api/";
        private string customUserAgent = "Foodapp/Testing Nomail";

        public List<Ingredient> GetAllIngredients(string name)
        {
            throw new NotImplementedException();
        }

        public Ingredient GetFirstIngredient(string name)
        {
            throw new NotImplementedException();
        }

        public bool TestConnection()
        {
            throw new NotImplementedException();
        }
    }
}
