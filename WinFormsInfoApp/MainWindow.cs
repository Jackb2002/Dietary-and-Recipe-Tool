using RecipeExtractor;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using WinFormsInfoApp.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static WinFormsInfoApp.IIngredientContext;

namespace WinFormsInfoApp
{
    /// <summary>
    /// Represents the main window of the application.
    /// </summary>
    public partial class MainWindow : Form
    {
        private const string _recipe_FilePath = "recipe_cache.json";
        private const string _ingredient_FilePath = "ingredient_cache.json";
        private readonly IIngredientContext _ingredientContext;
        private readonly List<Recipe> _recipes = [];
        private List<Ingredient> _ingredientCache = [];
        private Recipe? CurrentRecipeSelection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="ingredientContext">The ingredient context.</param>
        public MainWindow(IIngredientContext ingredientContext)
        {
            _ingredientContext = ingredientContext;
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for the main window load event.
        /// </summary>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            // Display connection status based on the connection type
            if (_ingredientContext.connectionType == ConnectionType.Local)
            {
                ConnectionStatus.Text = "Connected to local DB";
                _ = MessageBox.Show("A local connection is being used, some functionality may be limited");
                ConnectionStatus.ForeColor = Color.Orange;
            }
            else
            {
                ConnectionStatus.Text = "Connected to API";
                ConnectionStatus.ForeColor = Color.Green;
            }

            // Load recipes asynchronously
            BackgroundWorker recipeLoader = new();
            recipeLoader.DoWork += new DoWorkEventHandler(LoadRecipes);
            recipeLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadRecipesCompleted);
            recipeLoader.RunWorkerAsync();

            // Load ingredients asynchronously
            BackgroundWorker ingredientLoader = new();
            ingredientLoader.DoWork += new DoWorkEventHandler(LoadIngredients);
            ingredientLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadIngredientsCompleted);
            ingredientLoader.RunWorkerAsync();

            Debug.WriteLine("Loaded recipes and ingredients");
        }

        /// <summary>
        /// Event handler for the completion of recipe loading.
        /// </summary>
        private void LoadRecipesCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Loaded {_recipes.Count} recipes successfully");
            recipeList.Items.AddRange(_recipes.Select(x => x.Title).ToArray());
        }

        /// <summary>
        /// Event handler for the completion of ingredient loading.
        /// </summary>
        private void LoadIngredientsCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Loaded {_ingredientCache.Count} ingredients successfully");
        }

        /// <summary>
        /// Method to load recipes asynchronously.
        /// </summary>
        private void LoadRecipes(object? sender, DoWorkEventArgs e)
        {
            ImportRecipes();
        }

        /// <summary>
        /// Method to load ingredients asynchronously.
        /// </summary>
        private void LoadIngredients(object? sender, DoWorkEventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory, _ingredient_FilePath);
            JsonSerializerHelper helper = new();
            List<Ingredient> localIngredients = helper.DeserializeIngredients(path);
            _ingredientCache = localIngredients;
        }

        /// <summary>
        /// Imports recipes from a file.
        /// </summary>
        /// <param name="filePath">The path to the file containing recipes.</param>
        /// <returns>A list of imported recipes.</returns>
        public void ImportRecipes()
        {
            _recipes.AddRange(ImportLocalRecipes(_recipe_FilePath));
            int counter = 0;
            int total = _recipes.Count;
            foreach (Recipe recipe in _recipes)
            {
                counter++;
                if(recipe.Kcal == default)
                {
                    Debug.WriteLine($"Looking for recipe nutrition information for new recipe number {counter} of {total}");
                    Recipe newRecipe = ExtractRecipeFromURL(recipe.RecipeUrls);
                    if (newRecipe != null)
                    {
                        if(newRecipe.Kcal == default)
                        {
                            continue;
                        }
                        else
                        {
                            Debug.WriteLine($"Found recipe nutrition information for new recipe number {counter} of {total}");
                            recipe.Kcal = newRecipe.Kcal;
                            recipe.Fat = newRecipe.Fat;
                            recipe.Saturates = newRecipe.Saturates;
                            recipe.Carbs = newRecipe.Carbs;
                            recipe.Sugars = newRecipe.Sugars;
                            recipe.Fibre = newRecipe.Fibre;
                            recipe.Protein = newRecipe.Protein;
                            recipe.Salt = newRecipe.Salt;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Imports local recipes from a JSON file and filters out duplicates.
        /// </summary>
        /// <param name="fileName">The name of the JSON file.</param>
        /// <returns>A collection of imported local recipes.</returns>
        private IEnumerable<Recipe> ImportLocalRecipes(string fileName)
        {
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            JsonSerializerHelper helper = new();
            List<Recipe> localRecipes = helper.DeserializeRecipes(path);


            //Filter out duplicates
            localRecipes = localRecipes.Distinct().ToList();

            //Filter out duplicates and empty titles
            localRecipes = localRecipes.Where(x => x.Title != "").ToList();
            
            //Filter out recipes with no ingredients or unspecified ingredients
            localRecipes = localRecipes.Where(x => x.Ingredients != ""
                && x.Ingredients != "Not specified").ToList();

            return localRecipes;
        }

        /// <summary>
        /// Handles closing the form, saves the current entries to cache.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("Form is closing, saving current entries to cache");
            JsonSerializerHelper helper = new();
            helper.SerializeIngredients(_ingredientCache, _ingredient_FilePath);
            helper.SerializeRecipes(_recipes, _recipe_FilePath);
            Debug.WriteLine($"Saved {_ingredientCache.Count} ingredients to cache");
            Debug.WriteLine($"Saved {_recipes.Count} recipes to cache");
        }

        /// <summary>
        /// Event handler for the recipe list selection change event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recipeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recipe recipe = _recipes[recipeList.SelectedIndex];
            CurrentRecipeSelection = recipe;
            recipeTitle.Text = "Recipe Name: " + CurrentRecipeSelection.Title;
            recipeLink.Text = "Recipe Link: Here";
            recipeLink.Click += LaunchRecipeOnClick(CurrentRecipeSelection.RecipeUrls);
            recipeIng.Text = "Recipe Ingredients: " + 
                Environment.NewLine + CurrentRecipeSelection.Ingredients;
        }

        private EventHandler LaunchRecipeOnClick(string url)
        {
            return (sender, e) => Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }

        /// <summary>
        /// Uses the RecipeExtractor to extract a recipe from a URL, using a good food URL
        /// </summary>
        /// <param name="url">URL to a good food web page with a recipe</param>
        /// <returns>Recipe object or null in case of error</returns>
        private static Recipe ExtractRecipeFromURL(string url)
        {
            var recipeRaw = GoodFood.ParseRecipeFromUrl(url);
            if (recipeRaw != null)
            {
                return CreateRecipeFromParsedKeyvalues(recipeRaw);
            }
            return null;
        }

        private static Recipe CreateRecipeFromParsedKeyvalues(List<KeyValuePair<string, object>> recipeRaw)
        {
            Recipe recipe = new Recipe();

            recipe.Title = GetValue<string>(recipeRaw, "name");
            recipe.Difficulty = GetValue<string>(recipeRaw, "difficulty");
            recipe.Serves = GetValue<string>(recipeRaw, "serves");
            recipe.Rating = GetValue<string>(recipeRaw, "rating");
            recipe.Reviews = GetValue<string>(recipeRaw, "reviews");
            recipe.Vegetarian = GetValue<bool>(recipeRaw, "vegetarian");
            recipe.Vegan = GetValue<bool>(recipeRaw, "vegan");
            recipe.DairyFree = GetValue<bool>(recipeRaw, "dairy_free");
            recipe.Keto = GetValue<bool>(recipeRaw, "keto");
            recipe.GlutenFree = GetValue<bool>(recipeRaw, "gluten_free");
            recipe.PrepTime = GetValue<string>(recipeRaw, "prep_time");
            recipe.CookTime = GetValue<string>(recipeRaw, "cook_time");
            recipe.Ingredients = GetValue<string>(recipeRaw, "ingredients");
            recipe.RecipeUrls = GetValue<string>(recipeRaw, "recipe_urls");

            if (float.TryParse(GetValue<string>(recipeRaw, "kcal"), out float kcal)) recipe.Kcal = kcal;
            if (float.TryParse(GetValue<string>(recipeRaw, "fat"), out float fat)) recipe.Fat = fat;
            if (float.TryParse(GetValue<string>(recipeRaw, "saturates"), out float saturates)) recipe.Saturates = saturates;
            if (float.TryParse(GetValue<string>(recipeRaw, "carbs"), out float carbs)) recipe.Carbs = carbs;
            if (float.TryParse(GetValue<string>(recipeRaw, "sugars"), out float sugars)) recipe.Sugars = sugars;
            if (float.TryParse(GetValue<string>(recipeRaw, "fibre"), out float fibre)) recipe.Fibre = fibre;
            if (float.TryParse(GetValue<string>(recipeRaw, "protein"), out float protein)) recipe.Protein = protein;
            if (float.TryParse(GetValue<string>(recipeRaw, "salt"), out float salt)) recipe.Salt = salt;

            recipe.Description = GetValue<string>(recipeRaw, "description");
            recipe.Method = GetValue<string>(recipeRaw, "method");

            return recipe;
        }


        private static T? GetValue<T>(List<KeyValuePair<string, object>> recipeRaw, string key)
        {
            var pair = recipeRaw.Where(x => x.Key.ToLower() == key).FirstOrDefault();
            if (pair.Value != null && pair.Value is T)
            {
                return (T)pair.Value;
            }
            return default;
        }
    }
}
