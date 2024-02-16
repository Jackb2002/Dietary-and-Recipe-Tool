using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }

        private void worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            Debug.WriteLine("Progress: " + e.ProgressPercentage + "%");
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
            worker?.ReportProgress(10);

            if(File.Exists(path))
            {
                Debug.WriteLine("Database exists");
                worker?.ReportProgress(50);
            }
            else
            {
                Debug.WriteLine("Database does not exist");
                worker?.ReportProgress(25);
                Debug.WriteLine("Creating database...");
                
                worker?.ReportProgress(50);
                Debug.WriteLine("Database created");
            }


            worker?.ReportProgress(100);
        }
    }
}
