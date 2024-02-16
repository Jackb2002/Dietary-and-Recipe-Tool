using SupermarketInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InfoApp
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
            fileCheckerUpdater.DoWork += worker_DoWork;
            fileCheckerUpdater.WorkerReportsProgress = true;
            fileCheckerUpdater.ProgressChanged += worker_ProgressChanged;
            fileCheckerUpdater.RunWorkerCompleted += worker_RunWorkerCompleted;
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
                int maxItems = Tesco.GetMaxItems(line[0].Trim());
                url_list[i] = new SupermarketLink(line[0].Trim(), line[1].Trim());
                Debug.WriteLine(string.Format("Supermarket: {0}, URL: {1}", 
                    url_list[i].Store, url_list[i].Link));
            }

            return url_list;
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
            List<string> tesco_urls = new List<string>();
            foreach (SupermarketLink link in url_list)
            {
                switch (link.Store)
                {
                    case "Tesco":
                        tesco_urls.Add(link.Link);
                        break;
                }
            }

            Tesco.DownloadTescoURLs(tesco_urls.ToArray());
        }
    }
}
