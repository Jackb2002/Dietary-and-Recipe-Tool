using AngleSharp.Common;
using RecipeExtractor;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using Windows.Media.Playback;
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
        private Recipe? CurrentRecipeSelection;
        private Family.Family currentFamily;
        private bool changingWeights = false;
        private bool changingLiquids = false;
        private Ingredient?[] currentIngredients;

        internal readonly List<Recipe> _recipesCache = [];
        internal readonly List<Diet> _dietCache = [];
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

            SetTodaysMeal();

            Debug.WriteLine("Loaded recipes and ingredients");
        }

        private void LoadDietsCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Loaded {_dietCache.Count} diets successfully");
        }

        private void LoadDiets(object? sender, DoWorkEventArgs e)
        {
            ImportDiets();
            var customInuse = _dietCache.FirstOrDefault(x => x.InUse);
            if (customInuse != default)
            {
                currentDiet = customInuse;
                SetTodaysMeal();
            }
            else
            {
                Debug.WriteLine("Got list of diets with no current diet selected");
            }
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

            // Load ingredients asynchronously
            BackgroundWorker ingredientLoader = new();
            ingredientLoader.DoWork += new DoWorkEventHandler(LoadIngredients);
            ingredientLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadIngredientsCompleted);
            ingredientLoader.RunWorkerAsync();
        }

        /// <summary>
        /// Event handler for the completion of ingredient loading.
        /// </summary>
        private void LoadIngredientsCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine($"Loaded {_ingredientCache.Count} ingredients successfully");


            // Load diets asynchronously
            BackgroundWorker dietLoader = new();
            dietLoader.DoWork += new DoWorkEventHandler(LoadDiets);
            dietLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadDietsCompleted);
            dietLoader.RunWorkerAsync();
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
            helper.SerializeDiets(_dietCache.Where(x => (x.DefaultDiet == false) || (x.DefaultDiet == true && x.InUse == true)).ToList(), _diet_FilePath); //only save default diets if in use 
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


            //Decode HTML strings
            string decodedName = WebUtility.HtmlDecode(recipe.Title);
            recipe.Title = decodedName;
            string decodedIngredients = WebUtility.HtmlDecode(recipe.Ingredients);
            recipe.Ingredients = decodedIngredients;
            string decodedMethod = WebUtility.HtmlDecode(recipe.Method);
            recipe.Method = decodedMethod;
            string decodedDescription = WebUtility.HtmlDecode(recipe.Description);
            recipe.Description = decodedDescription;

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
                                g.DrawString("Kcal: " + CurrentRecipeSelection.Kcal + " kcal", font, b, textRect, stringFormat);
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
            currentIngredients = _ingredientContext.GetIngredientsByName(ing_name);
            ingComboBox.Items.Clear();
            if (currentIngredients.Length > 0)
            {
                ingComboBox.Items.AddRange(currentIngredients.Select(x => x.Name).ToArray());
                ingComboBox.SelectedIndex = 0;
            }
            else
            {
                _ = MessageBox.Show("No ingredients found with that name");
            }
            if(currentIngredients.Length > 0)
            {
                DisplayIngredient(currentIngredients[0]);
            }
        }

        private void DisplayIngredient(Ingredient ing)
        {
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
            ExistingDietsSelector selector = new(this);
            _ = selector.ShowDialog();
            if (selector.DialogResult == DialogResult.OK)
            {
                SetupNewDiet();
            }
        }

        private void customDiet_Click(object sender, EventArgs e)
        {
            CustomDietsCreator creator = new(this);
            _ = creator.ShowDialog();
            if (creator.DialogResult == DialogResult.OK)
            {
                SetupNewDiet();
            }
        }

        private void SetupNewDiet()
        {
            Debug.WriteLine("New diet selected " + currentDiet.Name);
            currentDiet.RecipeRank = Diet.GenerateMeals(currentDiet, _recipesCache);
            Debug.WriteLine("Diet now has " + currentDiet.RecipeRank.Count + " recipes");
            currentDiet.StartDate = DateTime.Now;
            _dietCache.Add(currentDiet);
            SetTodaysMeal();
        }

        private void SetTodaysMeal()
        {
            if (currentDiet == null)
            {
                return;
            }

            // Update UI elements on the UI thread
            Invoke(new MethodInvoker(delegate
            {
                foreach (var diet in _dietCache)
                {
                    diet.InUse = false;
                }
                currentDiet.InUse = true;
                _dietCache.Where(x => x.Name == currentDiet.Name).FirstOrDefault().InUse = true;
                currentDietLabel.Text = "Current Diet: " + currentDiet.Name;
                int daysSinceDietStart = (DateTime.Now - currentDiet.StartDate).Days;
                currentDiet.RecipeRank = Diet.GenerateMeals(currentDiet, _recipesCache);
                int mealIndex = daysSinceDietStart % currentDiet.RecipeRank.Count; // Wrap around
                Recipe todaysMeal = currentDiet.RecipeRank.GetItemByIndex(mealIndex).Key; // Get the recipe for todays index
                int listIndex = _recipesCache.IndexOf(todaysMeal);
                recipeList.SelectedIndex = listIndex;
                nextMealLabel.Text = "Today's Meal: " + todaysMeal.Title;
            }));

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

        private void measurementWeightChanged(object sender, EventArgs e)
        {
            if (changingWeights)
            { // flag to prevent infinite loop
                return;
            }

            Control control = (Control)sender; // Get the control that triggered the event
            string controlName = control.Name;
            if (double.TryParse(control.Text.Trim(), out double value))
            {
                switch (controlName)
                {
                    case "ozTxt":
                        value = KitchenConverter.OuncesToGrams(value);
                        break;
                    case "lbsTxt":
                        value = KitchenConverter.PoundsToGrams(value);
                        break;
                    case "tspTxt":
                        value = KitchenConverter.TeaspoonsToGrams(value);
                        break;
                    case "tbspTxt":
                        value = KitchenConverter.TablespoonsToGrams(value);
                        break;
                    case "gTxt":
                        break; // Already in grams
                    default:
                        Debug.WriteLine("Unknown control name " + controlName);
                        break;
                }

                SetAllWeightMeasurements(value);
            }
        }

        /// <summary>
        /// Set all weight measurements with a known grams quantity
        /// </summary>
        /// <param name="valueInGrams">The weight in grams</param>
        private void SetAllWeightMeasurements(double valueInGrams)
        {
            changingWeights = true;
            valueInGrams = Math.Round(valueInGrams, 5);
            double oz = Math.Round(KitchenConverter.GramsToOunces(valueInGrams), 5);
            double lbs = Math.Round(KitchenConverter.GramsToPounds(valueInGrams), 5);
            double tsp = Math.Round(KitchenConverter.GramsToTeaspoons(valueInGrams), 5);
            double tbsp = Math.Round(KitchenConverter.GramsToTablespoons(valueInGrams), 5);
            gTxt.Text = valueInGrams.ToString();
            ozTxt.Text = oz.ToString();
            lbsTxt.Text = lbs.ToString();
            tspTxt.Text = tsp.ToString();
            tbspTxt.Text = tbsp.ToString();
            changingWeights = false;
        }

        private void SetAllLiquidMeasurements(double valueInMl)
        {
            changingLiquids = true;
            valueInMl = Math.Round(valueInMl, 5);
            double liters = Math.Round(KitchenConverter.MillilitersToLitres(valueInMl), 5);
            double flOz = Math.Round(KitchenConverter.MillilitersToFluidOunces(valueInMl), 5);
            double cups = Math.Round(KitchenConverter.MillilitersToCups(valueInMl), 5);
            lTxt.Text = liters.ToString();
            mlTxt.Text = valueInMl.ToString();
            flOzTxt.Text = flOz.ToString();
            cupTxt.Text = cups.ToString();
            changingLiquids = false;
        }

        private void measurementLiquidChanged(object sender, EventArgs e)
        {
            if (changingLiquids)
            { // flag to prevent infinite loop
                return;
            }

            Control control = (Control)sender; // Get the control that triggered the event
            string controlName = control.Name;

            if (double.TryParse(control.Text.Trim(), out double value))
            {
                switch (controlName)
                {
                    case "mlTxt":
                        break; // Already in ml
                    case "flOzTxt":
                        value = KitchenConverter.FluidOuncesToMilliliters(value);
                        break;
                    case "lTxt":
                        value = KitchenConverter.LitresToMilliliters(value);
                        break;
                    case "cupTxt":
                        value = KitchenConverter.CupsToMilliliters(value);
                        break;
                    default:
                        Debug.WriteLine("Unknown control name " + controlName);
                        break;
                }

                SetAllLiquidMeasurements(value);
            }
        }

        private void ingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(currentIngredients != null)
            {
                DisplayIngredient(currentIngredients[ingComboBox.SelectedIndex]);
            }
        }

        private static T? GetValue<T>(List<KeyValuePair<string, object>> recipeRaw, string key)
        {
            KeyValuePair<string, object> pair = recipeRaw.Where(x => x.Key.ToLower() == key).FirstOrDefault();
            return pair.Value is not null and T ? (T)pair.Value : default;
        }
    }
}
