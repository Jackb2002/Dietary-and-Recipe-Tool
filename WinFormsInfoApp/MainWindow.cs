using CsvHelper;
using System.ComponentModel;
using System.Globalization;
using static WinFormsInfoApp.IIngredientContext;

namespace WinFormsInfoApp
{
    public partial class MainWindow : Form
    {
        private readonly IIngredientContext _ingredientContext;
        private List<Recipe> _recipes;
        public MainWindow(IIngredientContext ingredientContext)
        {
            _ingredientContext = ingredientContext;
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if(_ingredientContext.connectionType == ConnectionType.Local)
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
            BackgroundWorker recipeLoader = new();
            recipeLoader.DoWork += new DoWorkEventHandler(LoadRecipes);
            recipeLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadRecipesCompleted);
            recipeLoader.RunWorkerAsync();
        }

        private void LoadRecipesCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Loaded {_recipes.Count} recipes successfully");
        }

        private void LoadRecipes(object? sender, DoWorkEventArgs e)
        {
            _recipes = ImportRecipes("recipe_data.csv");
        }

        public List<Recipe> ImportRecipes(string filePath)
        {
            List<Recipe> recipes;

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                recipes = csv.GetRecords<Recipe>().ToList();
            }

            return recipes;
        }
    }
}
