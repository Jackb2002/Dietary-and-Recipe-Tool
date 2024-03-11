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
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Loading complete");
        }

        private void worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;
            Debug.WriteLine("BW loading database path...");
            string path = Path.GetFullPath(GlobalSettings.LocalDatabaseFile);
            Debug.WriteLine("Database path: " + path);
            Debug.WriteLine("Checking connection to API possible...");
            OpenFoodAPI apiConncetion = new OpenFoodAPI();
            if (apiConncetion.TestConnection())
            {
                var result = apiConncetion.GetFirstIngredient("chocolate");
                if(result != null)
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
            MessageBox.Show("Remote connection successful! Using remote DB");

        }

        private static void LaunchLocal(string path)
        {
            Debug.WriteLine("API connection failed");
            Debug.WriteLine("Loading local database...");
            MessageBox.Show("Remote conenction failed! Using local DB");
            DatabaseFileOpener databaseFile = new DatabaseFileOpener(path);
            DatabaseManager database = databaseFile.CreateOrOpen();

            var ings_db = database.GetIngredientNameIdPairs();
            foreach (var ing in ings_db)
            {
                Debug.WriteLine(ing);
            }
        }
    }
}
