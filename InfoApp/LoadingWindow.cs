using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace InfoApp
{
    public partial class LoadingWindow : Form
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        private void LoadingWindow_Shown(object sender, EventArgs e)
        {
            BackgroundWorker fileCheckerUpdater = new BackgroundWorker();
            fileCheckerUpdater.DoWork += worker_DoWork;
            fileCheckerUpdater.WorkerReportsProgress = true;
            fileCheckerUpdater.ProgressChanged += worker_ProgressChanged;
            fileCheckerUpdater.RunWorkerCompleted += worker_RunWorkerCompleted;
            fileCheckerUpdater.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                Debug.WriteLine("Error: " + e.Error.Message);
            }
            else if(e.Cancelled)
            {
                Debug.WriteLine("Worker was cancelled");
            }
            else
            {
                Debug.WriteLine("Worker completed, downloaded and checked all supermarket info");
            }   
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Code for checking and then downloading the HTML for each page
        }
    }
}
