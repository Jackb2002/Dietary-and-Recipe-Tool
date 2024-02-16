using System.ComponentModel;
using System.Diagnostics;

namespace WinFormsInfoApp
{
    public partial class LoadingWindow : Form
    {
        private SupermarketLink[] url_list;

        public LoadingWindow()
        {
            InitializeComponent();
        }

        private void LoadingWindow_Shown(object sender, EventArgs e)
        {
            url_list = ExtractSupermarketList();

            BackgroundWorker fileCheckerUpdater = new BackgroundWorker();
            fileCheckerUpdater.DoWork += downloader_DoWork;
            fileCheckerUpdater.RunWorkerCompleted += downloader_RunWorkerCompleted;
            fileCheckerUpdater.RunWorkerAsync();
        }

        private static SupermarketLink[] ExtractSupermarketList()
        {
            SupermarketLink[] url_list;
            var info = File.ReadAllLines(GlobalSettings.SupermarketURLsFile);
            //Split each line info a supermarket link object
            url_list = new SupermarketLink[info.Length];
            for (int i = 0; i < info.Length; i++)
            {
                string[] line = info[i].Split(';');
                url_list[i] = new SupermarketLink(line[0].Trim(), line[1].Trim());
                Debug.WriteLine(string.Format("Supermarket: {0}, URL: {1}", 
                    url_list[i].Store, url_list[i].Link));
            }

            return url_list;
        }

        private void downloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                Debug.WriteLine("Error: " + e.Error.Message);
                progressBar.BackColor = System.Drawing.Color.Red;
                progressBar.Value = 100;
            }
            else if(e.Cancelled)
            {
                Debug.WriteLine("Worker was cancelled");
                progressBar.BackColor = System.Drawing.Color.Red;
                progressBar.Value = 100;

            }
            else
            {
                Debug.WriteLine("Worker completed, downloaded and checked all supermarket info");
                progressBar.Value = 50;
            }

            BackgroundWorker fileLoader = new BackgroundWorker();
            fileLoader.WorkerReportsProgress = true;
            fileLoader.DoWork += loader_DoWork;
            fileLoader.RunWorkerCompleted += lodaer_RunWorkerCompleted;
            fileLoader.ProgressChanged += loader_ProgressChanged;
        }

        private void loader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = (e.ProgressPercentage/100) * 50;
            progressBar.Value = 50 + progress;
        }

        private void lodaer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Loading Completed!");
        }

        private void loader_DoWork(object sender, DoWorkEventArgs e)
        {
            string localDbPath = GlobalSettings.LocalDatabasePath;
            Debug.WriteLine("Loading local database from: " + localDbPath);
        }

        private void downloader_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("Downloader disabled, using a local test database");
        }
    }
}
