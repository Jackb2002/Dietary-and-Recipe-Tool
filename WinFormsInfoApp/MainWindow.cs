﻿using CsvHelper;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;
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
        private List<Recipe> _recipes;
        private List<Ingredient> _ingredientCache;

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
                MessageBox.Show("A local connection is being used, some functionality may be limited");
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
        }

        /// <summary>
        /// Event handler for the completion of recipe loading.
        /// </summary>
        private void LoadRecipesCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Loaded {_recipes.Count} recipes successfully");
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
        private void LoadRecipes(object? sender, DoWorkEventArgs e) => ImportRecipes();

        /// <summary>
        /// Method to load ingredients asynchronously.
        /// </summary>
        private void LoadIngredients(object? sender, DoWorkEventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory, _ingredient_FilePath);
            JsonSerializerHelper helper = new JsonSerializerHelper();
            var localIngredients = helper.DeserializeIngredients(path);
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
            _recipes.AddRange(ImportCSVRecipes("recipe_data.csv"));
        }

        /// <summary>
        /// Imports local recipes from a JSON file and filters out duplicates.
        /// </summary>
        /// <param name="fileName">The name of the JSON file.</param>
        /// <returns>A collection of imported local recipes.</returns>
        private IEnumerable<Recipe> ImportLocalRecipes(string fileName)
        {
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            JsonSerializerHelper helper = new JsonSerializerHelper();
            var localRecipes = helper.DeserializeRecipes(path);
            return localRecipes;
        }

        /// <summary>
        /// Imports recipes from a CSV file.
        /// </summary>
        /// <param name="filePath">The path to the CSV file containing recipes.</param>
        /// <returns>A list of imported recipes.</returns>
        private List<Recipe> ImportCSVRecipes(string filePath)
        {
            List<Recipe> recipes;
            List<Recipe> filtered_recipes = new List<Recipe>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                recipes = csv.GetRecords<Recipe>().ToList();
            }
            foreach (var recipe in recipes)
            {
                var recipeContained = recipes.Where(x => x.Title == recipe.Title).FirstOrDefault();
                if(recipeContained == default)
                {
                    filtered_recipes.Add(recipe);
                }
            }
            return filtered_recipes;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("Form is closing, saving current entries to cache");
            JsonSerializerHelper helper = new JsonSerializerHelper();
            helper.SerializeIngredients(_ingredientCache, _ingredient_FilePath);
            helper.SerializeRecipes(_recipes, _recipe_FilePath);
            Debug.WriteLine($"Saved {_ingredientCache.Count} ingredients to cache");
            Debug.WriteLine($"Saved {_recipes.Count} recipes to cache");
        }
    }
}
