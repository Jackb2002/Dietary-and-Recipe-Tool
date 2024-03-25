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
        private Family.Family currentFamily;

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
            var recipes = ImportLocalRecipes(_recipe_FilePath);
            var valid_recipes = recipes.Where(x => x.Kcal > 0 && x.Serving > 0).ToList();
            Debug.WriteLine("Counted " + valid_recipes.Count + " recipes with kcal and serving");
            _recipes.AddRange(valid_recipes);
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
            localRecipes = localRecipes.Where(x => x.Ingredients is not ""
                and not "Not specified").ToList();

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
            servingsLabel.Text = "Recipe Servings: " + CurrentRecipeSelection.Serving;
            recipeInfoPanel.Invalidate();
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
        private static Recipe? ExtractRecipeFromURL(string url)
        {
            List<KeyValuePair<string, object>>? recipeRaw = GoodFood.ParseRecipeFromUrl(url);
            return recipeRaw != null ? CreateRecipeFromParsedKeyvalues(recipeRaw) : null;
        }

        private static Recipe CreateRecipeFromParsedKeyvalues(List<KeyValuePair<string, object>> recipeRaw)
        {
            Recipe recipe = new()
            {
                Title = GetValue<string>(recipeRaw, "name"),
                Difficulty = GetValue<string>(recipeRaw, "difficulty"),
                Rating = GetValue<string>(recipeRaw, "rating"),
                Reviews = GetValue<string>(recipeRaw, "reviews"),
                Vegetarian = GetValue<bool>(recipeRaw, "vegetarian"),
                Vegan = GetValue<bool>(recipeRaw, "vegan"),
                DairyFree = GetValue<bool>(recipeRaw, "dairy_free"),
                Keto = GetValue<bool>(recipeRaw, "keto"),
                GlutenFree = GetValue<bool>(recipeRaw, "gluten_free"),
                PrepTime = GetValue<string>(recipeRaw, "prep_time"),
                CookTime = GetValue<string>(recipeRaw, "cook_time"),
                Ingredients = GetValue<string>(recipeRaw, "ingredients"),
                RecipeUrls = GetValue<string>(recipeRaw, "recipe_urls"),
            };

            if (float.TryParse(GetValue<string>(recipeRaw, "kcal"), out float kcal))
            {
                recipe.Kcal = kcal;
            }

            if (float.TryParse(GetValue<string>(recipeRaw, "fat"), out float fat))
            {
                recipe.Fat = fat;
            }

            if (float.TryParse(GetValue<string>(recipeRaw, "saturates"), out float saturates))
            {
                recipe.Saturates = saturates;
            }

            if (float.TryParse(GetValue<string>(recipeRaw, "carbs"), out float carbs))
            {
                recipe.Carbs = carbs;
            }

            if (float.TryParse(GetValue<string>(recipeRaw, "sugars"), out float sugars))
            {
                recipe.Sugars = sugars;
            }

            if (float.TryParse(GetValue<string>(recipeRaw, "fibre"), out float fibre))
            {
                recipe.Fibre = fibre;
            }

            if (float.TryParse(GetValue<string>(recipeRaw, "protein"), out float protein))
            {
                recipe.Protein = protein;
            }

            if (float.TryParse(GetValue<string>(recipeRaw, "salt"), out float salt))
            {
                recipe.Salt = salt;
            }
            if (int.TryParse(GetValue<string>(recipeRaw, "serving"), out int serves))
            {
                recipe.Serving = serves;
            }

            recipe.Description = GetValue<string>(recipeRaw, "description");
            recipe.Method = GetValue<string>(recipeRaw, "method");
            return recipe;
        }
        private void recipeInfoPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);
            Font font = new Font("Arial", 10);
            Size size = recipeInfoPanel.ClientSize; // Use ClientSize for size calculations
            int boxWidth = size.Width / 4;
            int boxHeight = size.Height / 2;
            int padding_x = 10;
            int padding_y = 10;

            if (CurrentRecipeSelection != null)
            {
                using (StringFormat stringFormat = new StringFormat())
                {
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    // Draw a box with padding 2 rows 4 columns 
                    for (int row = 0; row < 2; row++)
                    {
                        for (int col = 0; col < 4; col++)
                        {
                            int x = col * boxWidth + padding_x;
                            int y = row * boxHeight + padding_y;

                            // Define the rectangle for the text
                            RectangleF textRect = new RectangleF(x + 5, y + 5, boxWidth - 2 * padding_x, boxHeight - 2 * padding_y);

                            // Draw the rectangle
                            g.DrawRectangle(pen, x, y, boxWidth - padding_x, boxHeight - padding_y);

                            // Draw the nutrient information with text wrapping
                            switch (row * 4 + col)
                            {
                                case 0:
                                    g.DrawString("Kcal: " + CurrentRecipeSelection.Kcal, font, Brushes.Black, textRect, stringFormat);
                                    break;
                                case 1:
                                    g.DrawString("Fat: " + CurrentRecipeSelection.Fat, font, Brushes.Black, textRect, stringFormat);
                                    break;
                                case 2:
                                    g.DrawString("Saturates: " + CurrentRecipeSelection.Saturates, font, Brushes.Black, textRect, stringFormat);
                                    break;
                                case 3:
                                    g.DrawString("Carbs: " + CurrentRecipeSelection.Carbs, font, Brushes.Black, textRect, stringFormat);
                                    break;
                                case 4:
                                    g.DrawString("Sugars: " + CurrentRecipeSelection.Sugars, font, Brushes.Black, textRect, stringFormat);
                                    break;
                                case 5:
                                    g.DrawString("Fibre: " + CurrentRecipeSelection.Fibre, font, Brushes.Black, textRect, stringFormat);
                                    break;
                                case 6:
                                    g.DrawString("Protein: " + CurrentRecipeSelection.Protein, font, Brushes.Black, textRect, stringFormat);
                                    break;
                                case 7:
                                    g.DrawString("Salt: " + CurrentRecipeSelection.Salt, font, Brushes.Black, textRect, stringFormat);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void editFamilyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Opening family edit form");
            FamilyEditor familyEditor = new(currentFamily);
            familyEditor.ShowDialog();
            if(familyEditor.DialogResult == DialogResult.OK)
            {
                currentFamily = familyEditor.family;
                Debug.WriteLine($"Family updated, family is now {currentFamily.Count} people!");
            }
        }

        private static T? GetValue<T>(List<KeyValuePair<string, object>> recipeRaw, string key)
        {
            KeyValuePair<string, object> pair = recipeRaw.Where(x => x.Key.ToLower() == key).FirstOrDefault();
            return pair.Value is not null and T ? (T)pair.Value : default;
        }
    }
}
