using Microsoft.Extensions.Configuration;
using System.Windows.Forms;

namespace WinFormsInfoApp
{
    internal static class Program
    { 
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigureDB();

            ApplicationConfiguration.Initialize();
            Application.Run(new LoadingWindow());
        }
    }
}