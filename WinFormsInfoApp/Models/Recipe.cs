using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsInfoApp.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string WebsiteURL { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public List<Diet>? Diets { get; set; }
    }
}
