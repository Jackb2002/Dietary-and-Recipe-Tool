using System.ComponentModel;
using System.Diagnostics;
using WinFormsInfoApp.LocalDatabase;
using WinFormsInfoApp.Models;
using WinFormsInfoApp.OpenFood;

namespace WinFormsInfoApp
{
    /// <summary>
    /// Represents the loading window of the application.
    /// </summary>
    public partial class LoadingWindow : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingWindow"/> class.
        /// </summary>
        public LoadingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the loading event of the form.
        /// </summary>
        private void LoadingWindow_Load(object sender, EventArgs e)
        {
            // Initialize and start the background worker
            BackgroundWorker worker = new();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the completion event of the background worker.
        /// </summary>
        private void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            // Display a message and set progress bar to completion
            Log("Loading complete", true);
            UpdateProgressBar(100);
        }

        /// <summary>
        /// Performs background work.
        /// </summary>
        private void worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            // Indicate progress and perform loading tasks
            Log("Locating Database", true);
            UpdateProgressBar(15);
            string path = Path.GetFullPath(GlobalSettings.LocalDatabaseFile);
            Log("Database path: " + path);
            Log("Checking connection to API possible...", true);
            UpdateProgressBar(30);
            OpenFoodAPI apiConnection = new();
            Log("Trying to connect to API...", true);
            if (apiConnection.TestConnection())
            {
                Log("API connection made, testing API", true);
                UpdateProgressBar(50);

                Ingredient? result = apiConnection.GetFirstIngredient("chocolate");
                if (result != null)
                {
                    Log("Connected to API successfully", true);
                    UpdateProgressBar(80);
                    LaunchRemote(apiConnection);
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

        /// <summary>
        /// Launches tasks that require remote API connection.
        /// </summary>
        private void LaunchRemote(OpenFoodAPI apiConnection)
        {
            // Perform tasks using remote API
            Log("API connection successful");
            Log("Using remote database...");
            UpdateProgressBar(100);
            MainWindow main = new(apiConnection);
        }

        /// <summary>
        /// Launches tasks using local database.
        /// </summary>
        private void LaunchLocal(string path)
        {
            // Perform tasks using local database
            Log("API connection failed, using a local copy!", true);
            UpdateProgressBar(60);
            DatabaseFileOpener databaseFile = new(path);
            DatabaseManager database = databaseFile.CreateOrOpen();

            KeyValuePair<int, string>[] ings_db = database.GetIngredientNameIdPairs();
            foreach (KeyValuePair<int, string> ing in ings_db)
            {
                Log(ing.ToString());
            }
            UpdateProgressBar(100);
            MainWindow main = new(database);
        }

        /// <summary>
        /// Logs a message and updates the log display.
        /// </summary>
        private void Log(string message, bool toWindow = false)
        {
            // Log the message and update log display if required
            message = message.Trim();
            Debug.WriteLine(message);
            if (toWindow)
            {
                UpdateProgLabel(message);
            }
        }

        /// <summary>
        /// Updates the progress bar with the specified value.
        /// </summary>
        private void UpdateProgressBar(int value)
        {
            // Update the progress bar value safely
            if (LoadingBar.InvokeRequired)
            {
                _ = LoadingBar.Invoke(new Action<int>(UpdateProgressBar), value);
            }
            else
            {
                // Ensure the value is within the range of the progress bar
                value = Math.Max(LoadingBar.Minimum, Math.Min(LoadingBar.Maximum, value));
                // Update the progress bar value
                LoadingBar.Value = value;
            }
        }

        /// <summary>
        /// Updates the log display with the specified text.
        /// </summary>
        private void UpdateProgLabel(string text)
        {
            // Update the log display safely
            if (progLog.InvokeRequired)
            {
                _ = progLog.Invoke(new Action<string>(UpdateProgLabel), text);
            }
            else
            {
                progLog.Text = text;
            }
        }
    }
}
