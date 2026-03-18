namespace WinFormsInfoApp.Models
{
    public class Recipe
    {
        public string Title { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public string Reviews { get; set; } = string.Empty;
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool DairyFree { get; set; }
        public bool Keto { get; set; }
        public bool GlutenFree { get; set; }
        public string PrepTime { get; set; } = string.Empty;
        public string CookTime { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public string RecipeUrls { get; set; } = string.Empty;
        public float Kcal { get; set; }
        public float Fat { get; set; }
        public float Saturates { get; set; }
        public float Carbs { get; set; }
        public float Sugars { get; set; }
        public float Fibre { get; set; }
        public float Protein { get; set; }
        public float Salt { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Serving { get; set; }

        /// <summary>
        /// Spoonacular recipe ID. Non-zero for recipes sourced from the Spoonacular API.
        /// Used to fetch full ingredient and method details on demand.
        /// </summary>
        public int SpoonacularId { get; set; }


        public static float MAX_KCAL { get; private set; }
        public static float MAX_FAT { get; private set; }
        public static float MAX_SATURATES { get; private set; }
        public static float MAX_CARBS { get; private set; }
        public static float MAX_SUGARS { get; private set; }
        public static float MAX_FIBRE { get; private set; }
        public static float MAX_PROTEIN { get; private set; }
        public static float MAX_SALT { get; private set; }

        public static void GenerateMaxValues(List<Recipe> recipes)
        {
            MAX_KCAL = recipes.Max(r => r.Kcal);
            MAX_FAT = recipes.Max(r => r.Fat);
            MAX_SATURATES = recipes.Max(r => r.Saturates);
            MAX_CARBS = recipes.Max(r => r.Carbs);
            MAX_SUGARS = recipes.Max(r => r.Sugars);
            MAX_FIBRE = recipes.Max(r => r.Fibre);
            MAX_PROTEIN = recipes.Max(r => r.Protein);
            MAX_SALT = recipes.Max(r => r.Salt);
        }
    }
}
