using RecipeExtractor;
using System.ComponentModel;
using System.Diagnostics;
using WinFormsInfoApp.Family;
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
        private const string _diet_FilePath = "diet_cache.json";
        private readonly IIngredientContext _ingredientContext;
        private List<Ingredient> _ingredientCache = [];
        private readonly List<Diet> _dietCache = [];
        private Recipe? CurrentRecipeSelection;
        private Family.Family currentFamily;

        internal readonly List<Recipe> _recipesCache = [];
        internal Diet currentDiet;
        internal List<string> avoid_ings = [];
        internal List<string> good_ings = [];


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

            currentFamily = new Family.Family
            {
                People = [new AdultMale()]
            };

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

            // Load diets asynchronously
            BackgroundWorker dietLoader = new();
            dietLoader.DoWork += new DoWorkEventHandler(LoadDiets);
            dietLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadDietsCompleted);
            dietLoader.RunWorkerAsync();

            Debug.WriteLine("Loaded recipes and ingredients");
        }

        private void LoadDietsCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Loaded {_dietCache.Count} diets successfully");
        }

        private void LoadDiets(object? sender, DoWorkEventArgs e)
        {
            ImportDiets();
        }

        private void ImportDiets()
        {
            string path = Path.Combine(Environment.CurrentDirectory, _diet_FilePath);
            JsonSerializerHelper helper = new();
            List<Diet> localDiets = helper.DeserializeDiets(path);
            _dietCache.AddRange(localDiets);
        }

        /// <summary>
        /// Event handler for the completion of recipe loading.
        /// </summary>
        private void LoadRecipesCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Loaded {_recipesCache.Count} recipes successfully");
            recipeList.Items.AddRange(_recipesCache.Select(x => x.Title).ToArray());
            Recipe.GenerateMaxValues(_recipesCache);
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
            List<Recipe> recipes = ImportLocalRecipes(_recipe_FilePath).ToList();
            int total = 0, counter = 0;
            total = recipes.Count;
            foreach (Recipe? item in recipes)
            {
                counter++;
                Debug.WriteLine($"Progress: {counter}/{total}");
                //Check if default or zero 
                if (item.Kcal == 0 || item.Serving == 0)
                {
                    //Extract recipe from URL
                    Recipe? extractedRecipe = ExtractRecipeFromURL(item.RecipeUrls);
                    item.Kcal = extractedRecipe?.Kcal ?? 0;
                    item.Serving = extractedRecipe?.Serving ?? 0;
                    item.Fat = extractedRecipe?.Fat ?? 0;
                    item.Saturates = extractedRecipe?.Saturates ?? 0;
                    item.Carbs = extractedRecipe?.Carbs ?? 0;
                    item.Sugars = extractedRecipe?.Sugars ?? 0;
                    item.Fibre = extractedRecipe?.Fibre ?? 0;
                    item.Protein = extractedRecipe?.Protein ?? 0;
                    item.Salt = extractedRecipe?.Salt ?? 0;
                }
            }

            List<Recipe> valid_recipes = recipes.Where(x => x.Kcal > 0 && x.Serving > 0).ToList();
            Debug.WriteLine("Counted " + valid_recipes.Count + " recipes with kcal and serving");
            _recipesCache.AddRange(valid_recipes);
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
            helper.SerializeRecipes(_recipesCache, _recipe_FilePath);
            helper.SerializeDiets(_dietCache, _diet_FilePath);
            Debug.WriteLine($"Saved {_ingredientCache.Count} ingredients to cache");
            Debug.WriteLine($"Saved {_recipesCache.Count} recipes to cache");
            Debug.WriteLine($"Saved {_dietCache.Count} diets to cache");
        }

        /// <summary>
        /// Event handler for the recipe list selection change event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recipeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recipe recipe = _recipesCache[recipeList.SelectedIndex];
            CurrentRecipeSelection = recipe;
            recipeTitle.Text = "Recipe Name: " + CurrentRecipeSelection.Title;
            recipeLink.Text = "Recipe Link: Here";
            recipeIng.Text = "Recipe Ingredients: " +
                Environment.NewLine + CurrentRecipeSelection.Ingredients;
            servingsLabel.Text = "Recipe Servings: " + CurrentRecipeSelection.Serving;
            recipeInfoPanel.Invalidate();
        }

        private void LaunchRecipeOnClick(string url)
        {
            _ = Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
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
            Pen pen = new(Color.Black, 2);
            Font font = new("Arial", 10);
            Size size = recipeInfoPanel.ClientSize; // Use ClientSize for size calculations
            int boxWidth = size.Width / 4;
            int boxHeight = size.Height / 2;
            int padding_x = 10;
            int padding_y = 10;

            if (CurrentRecipeSelection != null)
            {
                using StringFormat stringFormat = new();
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Center;

                // Draw a box with padding 2 rows 4 columns 
                for (int row = 0; row < 2; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        int x = (col * boxWidth) + padding_x;
                        int y = (row * boxHeight) + padding_y;

                        // Define the rectangle for the text
                        RectangleF textRect = new(x + 5, y + 5, boxWidth - (2 * padding_x), boxHeight - (2 * padding_y));

                        // Draw the rectangle
                        g.DrawRectangle(pen, x, y, boxWidth - padding_x, boxHeight - padding_y);

                        //Calculate the recommended daily intake for each of the nutrients 
                        float kcal = currentFamily.GetTotalKcal();
                        float fat = currentFamily.GetTotalFat();
                        float saturates = currentFamily.GetTotalSaturates();
                        float carbs = currentFamily.GetTotalCarbs();
                        float sugars = currentFamily.GetTotalSugar();
                        float fibre = currentFamily.GetTotalFibre();
                        float protein = currentFamily.GetTotalProtein();
                        float salt = currentFamily.GetTotalSalt();

                        //Use current recipe selection to turn these into a percentage of daily intake for the whole
                        //family based on the serving of the recipe and the nutritional info provided 
                        float kcalPerServing = CurrentRecipeSelection.Kcal / CurrentRecipeSelection.Serving;
                        float fatPerServing = CurrentRecipeSelection.Fat / CurrentRecipeSelection.Serving;
                        float saturatesPerServing = CurrentRecipeSelection.Saturates / CurrentRecipeSelection.Serving;
                        float carbsPerServing = CurrentRecipeSelection.Carbs / CurrentRecipeSelection.Serving;
                        float sugarsPerServing = CurrentRecipeSelection.Sugars / CurrentRecipeSelection.Serving;
                        float fibrePerServing = CurrentRecipeSelection.Fibre / CurrentRecipeSelection.Serving;
                        float proteinPerServing = CurrentRecipeSelection.Protein / CurrentRecipeSelection.Serving;
                        float saltPerServing = CurrentRecipeSelection.Salt / CurrentRecipeSelection.Serving;

                        //Calculate the percentage of daily intake for the whole family
                        float kcalPercentage = kcalPerServing / kcal * 100;
                        float fatPercentage = fatPerServing / fat * 100;
                        float saturatesPercentage = saturatesPerServing / saturates * 100;
                        float carbsPercentage = carbsPerServing / carbs * 100;
                        float sugarsPercentage = sugarsPerServing / sugars * 100;
                        float fibrePercentage = fibrePerServing / fibre * 100;
                        float proteinPercentage = proteinPerServing / protein * 100;
                        float saltPercentage = saltPerServing / salt * 100;

                        //Create the threshold values
                        const float RED_THRESHOLD = 75;
                        const float ORANGE_THRESHOLD = 45;
                        Brush b;
                        // Draw the nutrient information with text wrapping
                        switch ((row * 4) + col)
                        {
                            case 0:
                                b = kcalPercentage > RED_THRESHOLD ? Brushes.Red : kcalPercentage > ORANGE_THRESHOLD ? Brushes.Orange : Brushes.Green;
                                g.DrawString("Kcal: " + CurrentRecipeSelection.Kcal + 'g', font, b, textRect, stringFormat);
                                break;
                            case 1:
                                b = fatPercentage > RED_THRESHOLD ? Brushes.Red : fatPercentage > ORANGE_THRESHOLD ? Brushes.Orange : Brushes.Green;
                                g.DrawString("Fat: " + CurrentRecipeSelection.Fat + 'g', font, b, textRect, stringFormat);
                                break;
                            case 2:
                                b = saturatesPercentage > RED_THRESHOLD ? Brushes.Red : saturatesPercentage > ORANGE_THRESHOLD ? Brushes.Orange : Brushes.Green;
                                g.DrawString("Saturates: " + CurrentRecipeSelection.Saturates + 'g', font, b, textRect, stringFormat);
                                break;
                            case 3:
                                b = carbsPercentage > RED_THRESHOLD ? Brushes.Red : carbsPercentage > ORANGE_THRESHOLD ? Brushes.Orange : Brushes.Green;
                                g.DrawString("Carbs: " + CurrentRecipeSelection.Carbs + 'g', font, b, textRect, stringFormat);
                                break;
                            case 4:
                                b = sugarsPercentage > RED_THRESHOLD ? Brushes.Red : sugarsPercentage > ORANGE_THRESHOLD ? Brushes.Orange : Brushes.Green;
                                g.DrawString("Sugars: " + CurrentRecipeSelection.Sugars + 'g', font, b, textRect, stringFormat);
                                break;
                            case 5:
                                b = fibrePercentage > RED_THRESHOLD ? Brushes.Red : fibrePercentage > ORANGE_THRESHOLD ? Brushes.Orange : Brushes.Green;
                                g.DrawString("Fibre: " + CurrentRecipeSelection.Fibre + 'g', font, b, textRect, stringFormat);
                                break;
                            case 6:
                                b = proteinPercentage > RED_THRESHOLD ? Brushes.Red : proteinPercentage > ORANGE_THRESHOLD ? Brushes.Orange : Brushes.Green;
                                g.DrawString("Protein: " + CurrentRecipeSelection.Protein + 'g', font, b, textRect, stringFormat);
                                break;
                            case 7:
                                b = saltPercentage > RED_THRESHOLD ? Brushes.Red : saltPercentage > ORANGE_THRESHOLD ? Brushes.Orange : Brushes.Green;
                                g.DrawString("Salt: " + CurrentRecipeSelection.Salt + 'g', font, b, textRect, stringFormat);
                                break;
                        }

                    }
                }
            }
        }

        private void editFamilyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Opening family edit form");
            FamilyEditor familyEditor = new(currentFamily);
            _ = familyEditor.ShowDialog();
            if (familyEditor.DialogResult == DialogResult.OK)
            {
                currentFamily = familyEditor.family;
                Debug.WriteLine($"Family updated, family is now {currentFamily.Count} people!");
            }
        }

        private void recipeLink_Click(object sender, EventArgs e)
        {
            if (CurrentRecipeSelection != null)
            {
                LaunchRecipeOnClick(CurrentRecipeSelection.RecipeUrls);
            }
        }

        private void ingSearch_Click(object sender, EventArgs e)
        {
            string ing_name = ingName.Text;
            if (string.IsNullOrWhiteSpace(ing_name))
            {
                _ = MessageBox.Show("Please enter an ingredient name");
                return;
            }
            Ingredient? ing = _ingredientContext.GetFirstIngredient(ing_name);
            ingOutputBox.Text = ing != null
                ? $"" +
                    $"Ingredient Name - {ing.Name}\n" +
                    $"Ingredient Calorie Information - {ing.Calories} kcal\n" +
                    $"Ingredient Fat Information - {ing.Fat} g\n" +
                    $"Ingredient Carbohydrate Information - {ing.Carbohydrates} g\n" +
                    $"Ingredient Protein Information - {ing.Protein} g\n" +
                    $"Ingredient Sugar Information - {ing.Sugar} g\n" +
                    $"Ingredient Fibre Information - {ing.Fibre} g\n" +
                    $"Ingredient Weight Information - {ing.Product_Weight} g\n"
                : "Ingredient not found";
        }

        private void premadeDiet_Click(object sender, EventArgs e)
        {
            PremadeDietsSelector selector = new(this);
            _ = selector.ShowDialog();
            if (selector.DialogResult == DialogResult.OK)
            {
                Debug.WriteLine("New diet selected " + currentDiet.Name);
                currentDiet.RecipeRank = Diet.GenerateMeals(currentDiet, _recipesCache);
                Debug.WriteLine("Diet now has " + currentDiet.RecipeRank.Count + " recipes");
            }
        }

        private void customDiet_Click(object sender, EventArgs e)
        {
            CustomDietsCreator creator = new(this);
            _ = creator.ShowDialog();
            if (creator.DialogResult == DialogResult.OK)
            {
                Debug.WriteLine("New diet created " + currentDiet.Name);
                currentDiet.RecipeRank = Diet.GenerateMeals(currentDiet, _recipesCache);
                Debug.WriteLine("Diet now has " + currentDiet.RecipeRank.Count + " recipes");
            }
        }

        private void badIngredients_Click(object sender, EventArgs e)
        {
            IngredientSelector selector = new(false, this);
            _ = selector.ShowDialog();
            Debug.WriteLine("Avoid ingredients are now " + string.Join(" ", avoid_ings));
        }

        private void goodIngredients_Click(object sender, EventArgs e)
        {
            IngredientSelector selector = new(true, this);
            _ = selector.ShowDialog();
            Debug.WriteLine("Good ingredients are now " + string.Join(" ", good_ings));
        }

        private static T? GetValue<T>(List<KeyValuePair<string, object>> recipeRaw, string key)
        {
            KeyValuePair<string, object> pair = recipeRaw.Where(x => x.Key.ToLower() == key).FirstOrDefault();
            return pair.Value is not null and T ? (T)pair.Value : default;
        }
    }
}
