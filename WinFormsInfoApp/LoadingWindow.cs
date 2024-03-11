using System.ComponentModel;
using System.Diagnostics;
using WinFormsInfoApp.Database;
using WinFormsInfoApp.Models;
using WinFormsInfoApp.OpenFood;

namespace WinFormsInfoApp
{
    public partial class LoadingWindow : Form
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        private void LoadingWindow_Load(object sender, EventArgs e)
        {
            BackgroundWorker worker = new();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            _ = MessageBox.Show("Loading complete");
        }

        private void worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("BW loading database path...");
            string path = Path.GetFullPath(GlobalSettings.LocalDatabaseFile);
            Debug.WriteLine("Database path: " + path);
            Debug.WriteLine("Checking connection to API possible...");
            OpenFoodAPI apiConncetion = new();
            if (apiConncetion.TestConnection())
            {
                Ingredient? result = apiConncetion.GetFirstIngredient("chocolate");
                if (result != null)
                {
                    LaunchRemote(apiConncetion);
                }
                else
                {
                    LaunchLocal(path);
                }
            }
            else
            {
                LaunchLocal(path);
            }
        }

        private void LaunchRemote(OpenFoodAPI apiConncetion)
        {
            // Launch with openfoodfacts API
            Debug.WriteLine("API connection successful");
            Debug.WriteLine("Using remote database...");
            _ = MessageBox.Show("Remote connection successful! Using remote DB");

        }

        private static void LaunchLocal(string path)
        {
            Debug.WriteLine("API connection failed");
            Debug.WriteLine("Loading local database...");
            _ = MessageBox.Show("Remote conenction failed! Using local DB");
            DatabaseFileOpener databaseFile = new(path);
            DatabaseManager database = databaseFile.CreateOrOpen();

            KeyValuePair<int, string>[] ings_db = database.GetIngredientNameIdPairs();
            foreach (KeyValuePair<int, string> ing in ings_db)
            {
                Debug.WriteLine(ing);
            }
        }
    }
}
