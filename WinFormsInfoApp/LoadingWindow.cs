using System.ComponentModel;
using System.Diagnostics;
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
            apiConncetion.TestConnection();
            var result = apiConncetion.GetFirstIngredient("Salt");
            if(result == null)
            {
                Debug.WriteLine("API connection failed");
                Debug.WriteLine("Loading local database...");
                MessageBox.Show("Remote conenction failed! Using local DB");
            }
            else
            {
                Debug.WriteLine("API connection successful");
                Debug.WriteLine("Using remote database...");
            }
        }
    }
}
