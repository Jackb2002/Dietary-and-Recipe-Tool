using System.ComponentModel;
using System.Diagnostics;
using WinFormsInfoApp.Models;

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
            Debug.WriteLine("Checking database context");
            using (var context = new RecipeDbContext())
            {
                Debug.WriteLine("Database context OK");
                Debug.WriteLine("Adding initial ingredients");
                
                Ingredient flour = new Ingredient(0,"Flour", "Bag of flour",0.1,0.1,0.1,0.1,0.1,0.1,1000);
                Ingredient sugar = new Ingredient(0,"Sugar", "Bag of sugar",0,0,0,0,1000,0,1000);
                context.Ingredient.Add(sugar);
                context.Ingredient.Add(flour);

                context.SaveChanges();
            }
        }
    }
}
