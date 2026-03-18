using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RecipeExtractor;
using WinFormsInfoApp;
using WinFormsInfoApp.Family;
using WinFormsInfoApp.Models;
using static WinFormsInfoApp.IIngredientContext;

namespace DietaryApp.Avalonia.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private const string RecipeFilePath = "recipe_cache.json";
    private const string IngredientFilePath = "ingredient_cache.json";
    private const string DietFilePath = "diet_cache.json";

    private readonly IIngredientContext _ingredientContext;
    private readonly List<Recipe> _recipesCache = [];
    private readonly List<Diet> _dietCache = [];
    private List<Ingredient> _ingredientCache = [];
    private Ingredient?[]? _currentIngredients;
    private List<string> _shoppingList = [];
    private bool _changingWeights;
    private bool _changingLiquids;

    // ── Connection ──────────────────────────────────────────────────
    [ObservableProperty] private string _connectionStatus = string.Empty;
    [ObservableProperty] private bool _isLocalConnection;

    // ── Recipes ──────────────────────────────────────────────────────
    public ObservableCollection<Recipe> Recipes { get; } = [];
    [ObservableProperty] private Recipe? _selectedRecipe;
    [ObservableProperty] private string _recipeTitle = string.Empty;
    [ObservableProperty] private string _recipeIngredients = string.Empty;
    [ObservableProperty] private string _recipeLink = string.Empty;
    [ObservableProperty] private string _recipeServings = string.Empty;
    public ObservableCollection<NutrientDisplayItem> NutrientItems { get; } = [];

    // ── Ingredients ───────────────────────────────────────────────────
    [ObservableProperty] private string _ingredientSearchText = string.Empty;
    public ObservableCollection<string> IngredientResults { get; } = [];
    [ObservableProperty] private int _selectedIngredientIndex = -1;
    [ObservableProperty] private string _ingredientDetails = string.Empty;

    // ── Diet ──────────────────────────────────────────────────────────
    [ObservableProperty] private string _currentDietName = "No diet selected";
    [ObservableProperty] private string _todaysMeal = string.Empty;
    public List<string> AvoidIngredients { get; set; } = [];
    public List<string> GoodIngredients { get; set; } = [];
    internal Diet? CurrentDiet { get; private set; }
    internal Family CurrentFamily { get; private set; } = new() { People = [new AdultMale()] };

    // ── Shopping list ────────────────────────────────────────────────
    [ObservableProperty] private string _shoppingListText = string.Empty;

    // ── Weight converter ─────────────────────────────────────────────
    [ObservableProperty] private string _gTxt = string.Empty;
    [ObservableProperty] private string _ozTxt = string.Empty;
    [ObservableProperty] private string _lbsTxt = string.Empty;
    [ObservableProperty] private string _tspTxt = string.Empty;
    [ObservableProperty] private string _tbspTxt = string.Empty;

    // ── Liquid converter ──────────────────────────────────────────────
    [ObservableProperty] private string _mlTxt = string.Empty;
    [ObservableProperty] private string _lTxt = string.Empty;
    [ObservableProperty] private string _flOzTxt = string.Empty;
    [ObservableProperty] private string _cupTxt = string.Empty;

    public MainViewModel(IIngredientContext context)
    {
        _ingredientContext = context;
        IsLocalConnection = context.connectionType == ConnectionType.Local;
        ConnectionStatus = IsLocalConnection ? "Connected to local DB" : "Connected to API";
    }

    public async Task InitializeAsync()
    {
        await Task.Run(ImportRecipes);
        await global::Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
        {
            foreach (var r in _recipesCache) Recipes.Add(r);
            if (_recipesCache.Count > 0) Recipe.GenerateMaxValues(_recipesCache);
        });

        await Task.Run(ImportIngredients);
        await Task.Run(ImportDiets);
        SetTodaysMeal();
    }

    private void ImportRecipes()
    {
        var helper = new JsonSerializerHelper();
        string path = Path.Combine(Environment.CurrentDirectory, RecipeFilePath);
        var localRecipes = helper.DeserializeRecipes(path)
            .Distinct()
            .Where(x => x.Title != "" && x.Ingredients is not "" and not "Not specified")
            .ToList();

        foreach (var item in localRecipes)
        {
            if (item.Kcal == 0 || item.Serving == 0)
            {
                var extracted = ExtractRecipeFromUrl(item.RecipeUrls);
                if (extracted != null)
                {
                    item.Kcal = extracted.Kcal; item.Fat = extracted.Fat;
                    item.Saturates = extracted.Saturates; item.Carbs = extracted.Carbs;
                    item.Sugars = extracted.Sugars; item.Fibre = extracted.Fibre;
                    item.Protein = extracted.Protein; item.Salt = extracted.Salt;
                    item.Serving = extracted.Serving;
                }
            }
        }

        _recipesCache.AddRange(localRecipes.Where(x => x.Kcal > 0 && x.Serving > 0));
    }

    private void ImportIngredients()
    {
        var helper = new JsonSerializerHelper();
        string path = Path.Combine(Environment.CurrentDirectory, IngredientFilePath);
        _ingredientCache = helper.DeserializeIngredients(path);
    }

    private void ImportDiets()
    {
        var helper = new JsonSerializerHelper();
        string path = Path.Combine(Environment.CurrentDirectory, DietFilePath);
        var localDiets = helper.DeserializeDiets(path);
        _dietCache.AddRange(localDiets);

        var inUse = _dietCache.FirstOrDefault(x => x.InUse);
        if (inUse != null)
        {
            CurrentDiet = inUse;
            SetTodaysMeal();
        }
    }

    public void SaveCaches()
    {
        var helper = new JsonSerializerHelper();
        helper.SerializeIngredients(_ingredientCache, IngredientFilePath);
        helper.SerializeRecipes(_recipesCache, RecipeFilePath);
        helper.SerializeDiets(
            _dietCache.Where(x => !x.DefaultDiet || x.InUse).ToList(),
            DietFilePath);
    }

    // ── Recipe selection ──────────────────────────────────────────────
    partial void OnSelectedRecipeChanged(Recipe? value)
    {
        if (value == null) return;
        RecipeTitle = "Recipe Name: " + value.Title;
        RecipeLink = value.RecipeUrls;
        RecipeIngredients = "Ingredients:\n" + value.Ingredients;
        RecipeServings = "Servings: " + value.Serving;
        UpdateNutrientPanel(value);
    }

    private void UpdateNutrientPanel(Recipe recipe)
    {
        NutrientItems.Clear();
        if (recipe.Serving == 0) return;

        float fam_kcal = CurrentFamily.GetTotalKcal();
        float fam_fat = CurrentFamily.GetTotalFat();
        float fam_sat = CurrentFamily.GetTotalSaturates();
        float fam_carbs = CurrentFamily.GetTotalCarbs();
        float fam_sug = CurrentFamily.GetTotalSugar();
        float fam_fib = CurrentFamily.GetTotalFibre();
        float fam_pro = CurrentFamily.GetTotalProtein();
        float fam_salt = CurrentFamily.GetTotalSalt();

        float srv = recipe.Serving;
        NutrientItems.Add(MakeItem("Kcal",      $"{recipe.Kcal} kcal",  recipe.Kcal / srv / fam_kcal * 100));
        NutrientItems.Add(MakeItem("Fat",        $"{recipe.Fat}g",        recipe.Fat / srv / fam_fat * 100));
        NutrientItems.Add(MakeItem("Saturates",  $"{recipe.Saturates}g",  recipe.Saturates / srv / fam_sat * 100));
        NutrientItems.Add(MakeItem("Carbs",      $"{recipe.Carbs}g",      recipe.Carbs / srv / fam_carbs * 100));
        NutrientItems.Add(MakeItem("Sugars",     $"{recipe.Sugars}g",     recipe.Sugars / srv / fam_sug * 100));
        NutrientItems.Add(MakeItem("Fibre",      $"{recipe.Fibre}g",      recipe.Fibre / srv / fam_fib * 100));
        NutrientItems.Add(MakeItem("Protein",    $"{recipe.Protein}g",    recipe.Protein / srv / fam_pro * 100));
        NutrientItems.Add(MakeItem("Salt",       $"{recipe.Salt}g",       recipe.Salt / srv / fam_salt * 100));
    }

    private static NutrientDisplayItem MakeItem(string label, string value, float pct) => new()
    {
        Label = label,
        DisplayValue = value,
        Color = NutrientDisplayItem.BrushForPercentage(pct)
    };

    // ── Ingredient search ─────────────────────────────────────────────
    [RelayCommand]
    void SearchIngredient()
    {
        if (string.IsNullOrWhiteSpace(IngredientSearchText)) return;
        _currentIngredients = _ingredientContext.GetIngredientsByName(IngredientSearchText);
        IngredientResults.Clear();
        if (_currentIngredients != null && _currentIngredients.Length > 0)
        {
            foreach (var ing in _currentIngredients.Where(i => i != null))
                IngredientResults.Add(ing!.Name);
            SelectedIngredientIndex = 0;
        }
        else
        {
            IngredientDetails = "No ingredients found.";
        }
    }

    partial void OnSelectedIngredientIndexChanged(int value)
    {
        if (_currentIngredients == null || value < 0 || value >= _currentIngredients.Length) return;
        var ing = _currentIngredients[value];
        if (ing == null) return;
        IngredientDetails =
            $"Name: {ing.Name}\n" +
            $"Calories: {ing.Calories} kcal\n" +
            $"Fat: {ing.Fat} g\n" +
            $"Carbs: {ing.Carbohydrates} g\n" +
            $"Protein: {ing.Protein} g\n" +
            $"Sugar: {ing.Sugar} g\n" +
            $"Fibre: {ing.Fibre} g\n" +
            $"Weight: {ing.Product_Weight} g";
    }

    // ── Diet setup ────────────────────────────────────────────────────
    public void ApplyNewDiet(Diet diet)
    {
        CurrentDiet = diet;
        diet.RecipeRank = Diet.GenerateMeals(diet, _recipesCache);
        diet.StartDate = DateTime.Now;
        _dietCache.Add(diet);
        SetTodaysMeal();
    }

    private void SetTodaysMeal()
    {
        if (CurrentDiet == null || _recipesCache.Count == 0) return;

        foreach (var d in _dietCache) d.InUse = false;
        CurrentDiet.InUse = true;

        CurrentDiet.RecipeRank = Diet.GenerateMeals(CurrentDiet, _recipesCache);
        if (CurrentDiet.RecipeRank == null || CurrentDiet.RecipeRank.Count == 0) return;

        int days = (DateTime.Now - CurrentDiet.StartDate).Days;
        int mealIndex = days % CurrentDiet.RecipeRank.Count;
        var todaysMealRecipe = CurrentDiet.RecipeRank.ElementAt(mealIndex).Key;

        CurrentDietName = "Current Diet: " + CurrentDiet.Name;
        TodaysMeal = "Today's Meal: " + todaysMealRecipe.Title;

        var listIndex = _recipesCache.IndexOf(todaysMealRecipe);
        if (listIndex >= 0 && listIndex < Recipes.Count)
            SelectedRecipe = Recipes[listIndex];
    }

    // ── Family ────────────────────────────────────────────────────────
    public void UpdateFamily(Family family)
    {
        CurrentFamily = family;
        if (SelectedRecipe != null)
            UpdateNutrientPanel(SelectedRecipe);
    }

    // ── Shopping list ─────────────────────────────────────────────────
    [RelayCommand]
    void AddToShoppingList()
    {
        if (SelectedRecipe == null) return;
        var ings = SelectedRecipe.Ingredients.Split('\n').Select(x => x.Trim()).Where(x => x.Length > 0);
        _shoppingList.AddRange(ings);
        _shoppingList = _shoppingList.Distinct().ToList();
        _shoppingList.Sort();
        ShoppingListText = "-" + string.Join("\n-", _shoppingList);
    }

    [RelayCommand]
    void ClearShoppingList()
    {
        _shoppingList.Clear();
        ShoppingListText = string.Empty;
    }

    [RelayCommand]
    void SaveShoppingList()
    {
        if (string.IsNullOrWhiteSpace(ShoppingListText)) return;
        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "ShoppingList.txt");
        if (File.Exists(path)) File.Delete(path);
        File.WriteAllText(path, ShoppingListText);
    }

    // ── Weight converter ──────────────────────────────────────────────
    public void WeightFieldChanged(string fieldName, string rawValue)
    {
        if (_changingWeights) return;
        if (!double.TryParse(rawValue.Trim(), out double v)) return;

        double grams = fieldName switch
        {
            "oz"   => KitchenConverter.OuncesToGrams(v),
            "lbs"  => KitchenConverter.PoundsToGrams(v),
            "tsp"  => KitchenConverter.TeaspoonsToGrams(v),
            "tbsp" => KitchenConverter.TablespoonsToGrams(v),
            _      => v
        };

        _changingWeights = true;
        GTxt   = Math.Round(grams, 5).ToString();
        OzTxt  = Math.Round(KitchenConverter.GramsToOunces(grams), 5).ToString();
        LbsTxt = Math.Round(KitchenConverter.GramsToPounds(grams), 5).ToString();
        TspTxt = Math.Round(KitchenConverter.GramsToTeaspoons(grams), 5).ToString();
        TbspTxt= Math.Round(KitchenConverter.GramsToTablespoons(grams), 5).ToString();
        _changingWeights = false;
    }

    public void LiquidFieldChanged(string fieldName, string rawValue)
    {
        if (_changingLiquids) return;
        if (!double.TryParse(rawValue.Trim(), out double v)) return;

        double ml = fieldName switch
        {
            "floz" => KitchenConverter.FluidOuncesToMilliliters(v),
            "l"    => KitchenConverter.LitresToMilliliters(v),
            "cup"  => KitchenConverter.CupsToMilliliters(v),
            _      => v
        };

        _changingLiquids = true;
        MlTxt   = Math.Round(ml, 5).ToString();
        LTxt    = Math.Round(KitchenConverter.MillilitersToLitres(ml), 5).ToString();
        FlOzTxt = Math.Round(KitchenConverter.MillilitersToFluidOunces(ml), 5).ToString();
        CupTxt  = Math.Round(KitchenConverter.MillilitersToCups(ml), 5).ToString();
        _changingLiquids = false;
    }

    // ── URL launch ────────────────────────────────────────────────────
    [RelayCommand]
    void OpenRecipeLink()
    {
        if (!string.IsNullOrEmpty(RecipeLink))
            Process.Start(new ProcessStartInfo { FileName = RecipeLink, UseShellExecute = true });
    }

    // ── Helpers ───────────────────────────────────────────────────────
    private static Recipe? ExtractRecipeFromUrl(string url)
    {
        var raw = GoodFood.ParseRecipeFromUrl(url);
        if (raw == null) return null;
        var recipe = new Recipe { RecipeUrls = url };
        T? GetVal<T>(string key) { var p = raw.FirstOrDefault(x => x.Key.ToLower() == key); return p.Value is T t ? t : default; }

        recipe.Title       = GetVal<string>("name") ?? "";
        recipe.Ingredients = GetVal<string>("ingredients") ?? "";
        recipe.Method      = GetVal<string>("method") ?? "";
        recipe.Description = GetVal<string>("description") ?? "";
        recipe.Difficulty  = GetVal<string>("difficulty") ?? "";
        recipe.Rating      = GetVal<string>("rating") ?? "";
        recipe.Reviews     = GetVal<string>("reviews") ?? "";
        recipe.PrepTime    = GetVal<string>("prep_time") ?? "";
        recipe.CookTime    = GetVal<string>("cook_time") ?? "";

        if (float.TryParse(GetVal<string>("kcal"),      out float kcal))      recipe.Kcal = kcal;
        if (float.TryParse(GetVal<string>("fat"),       out float fat))       recipe.Fat = fat;
        if (float.TryParse(GetVal<string>("saturates"), out float sat))       recipe.Saturates = sat;
        if (float.TryParse(GetVal<string>("carbs"),     out float carbs))     recipe.Carbs = carbs;
        if (float.TryParse(GetVal<string>("sugars"),    out float sug))       recipe.Sugars = sug;
        if (float.TryParse(GetVal<string>("fibre"),     out float fib))       recipe.Fibre = fib;
        if (float.TryParse(GetVal<string>("protein"),   out float pro))       recipe.Protein = pro;
        if (float.TryParse(GetVal<string>("salt"),      out float salt))      recipe.Salt = salt;
        if (int.TryParse(GetVal<string>("serving"),     out int srv))         recipe.Serving = srv;

        return recipe;
    }
}
