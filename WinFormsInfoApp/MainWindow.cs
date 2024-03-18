using RecipeExtractor;
using System.ComponentModel;
using System.Diagnostics;
using WinFormsInfoApp.Models;
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
            
            //Filter out recipes with no ingredients
            localRecipes = localRecipes.Where(x => x.Ingredients != "").ToList();

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
            recipeTitle.Text = recipe.Title;
            RecipeCookTime.Text = string.IsNullOrWhiteSpace(recipe.CookTime) ? "Unknown" : recipe.CookTime;
        }

        /// <summary>
        /// Uses the RecipeExtractor to extract a recipe from a URL, using a good food URL
        /// </summary>
        /// <param name="url">URL to a good food web page with a recipe</param>
        /// <returns>Recipe object or null in case of error</returns>
        private static Recipe ExtractRecipeFromURL(string url)
        {
            string[] recipeRaw = GoodFood.ParseRecipeFromUrl(url);
            if (recipeRaw != null)
            {
                return ExtractRecipeFromString(recipeRaw);
            }
            return null;
        }

        private static Recipe ExtractRecipeFromString(string[] recipeRaw)
        {
            Recipe recipe = new()
            {
                Title = recipeRaw[0]?.ToString(),
                Difficulty = recipeRaw[1]?.ToString(),
                Serves = recipeRaw[2]?.ToString(),
                Rating = recipeRaw[3]?.ToString(),
                Reviews = recipeRaw[4]?.ToString(),
                Vegetarian = recipeRaw[5] == "true" ? true : false,
                Vegan = recipeRaw[6] == "true" ? true : false,
                DairyFree = recipeRaw[7] == "true" ? true : false,
                Keto = recipeRaw[8] == "true" ? true : false,
                GlutenFree = recipeRaw[9] == "true" ? true : false,
                PrepTime = recipeRaw[10]?.ToString(),
                CookTime = recipeRaw[11]?.ToString(),
                Ingredients = recipeRaw[12]?.ToString(),
                RecipeUrls = recipeRaw[13]?.ToString()
            };
            return recipe;
        }
    }
}
