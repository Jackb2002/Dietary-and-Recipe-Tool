using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;

namespace SupermarketInfo
{
    /// <summary>
    /// Event arguments for download progress events.
    /// </summary>
    public class DownloadProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Index of the page being downloaded.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Percentage of completion for the download operation.
        /// </summary>
        public double ProgressPercentage { get; set; }
    }

    /// <summary>
    /// Provides methods to download web pages and notifies subscribers about download progress.
    /// </summary>
    public static class PageDownloader
    {
        private static IWebDriver driver; // WebDriver instance for downloading pages

        /// <summary>
        /// Event raised to notify subscribers about download progress.
        /// </summary>
        public static event EventHandler<DownloadProgressEventArgs> DownloadProgress;

        /// <summary>
        /// Downloads the HTML source of a single web page.
        /// </summary>
        /// <param name="pageUrl">The URL of the web page to download.</param>
        /// <returns>The HTML source of the web page.</returns>
        public static string DownloadPage(string pageUrl)
        {
            InitializeWebDriver();
            try
            {
                driver.Navigate().GoToUrl(pageUrl);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                return driver.PageSource;
            }
            catch (WebDriverException ex)
            {
                Debug.WriteLine("WebDriver exception occurred:");
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }
            finally
            {
                DisposeWebDriver();
            }
        }

        /// <summary>
        /// Downloads the HTML source of multiple web pages.
        /// </summary>
        /// <param name="pageUrls">Array of URLs of the web pages to download.</param>
        /// <returns>An array of HTML sources of the web pages.</returns>
        public static string[] DownloadPages(string[] pageUrls)
        {
            InitializeWebDriver();
            try
            {
                string[] htmlSources = new string[pageUrls.Length];
                for (int i = 0; i < pageUrls.Length; i++)
                {
                    driver.Navigate().GoToUrl(pageUrls[i]);
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                    htmlSources[i] = driver.PageSource;

                    // Notify subscribers about the download progress
                    OnDownloadProgress(i, (double)(i + 1) / pageUrls.Length * 100);
                }
                return htmlSources;
            }
            catch (WebDriverException ex)
            {
                Debug.WriteLine("WebDriver exception occurred:");
                Debug.WriteLine(ex.Message);
                return new string[0];
            }
            finally
            {
                DisposeWebDriver();
            }
        }

        /// <summary>
        /// Initializes the WebDriver instance.
        /// </summary>
        private static void InitializeWebDriver()
        {
            try
            {
                driver = new ChromeDriver("chromedriver.exe");
            }
            catch (InvalidOperationException error)
            {
                Debug.WriteLine("ChromeDriver is not found or incorrect version.");
                Debug.WriteLine(error.Message);
                // Consider throwing an exception here to propagate the error
            }
        }

        /// <summary>
        /// Disposes of the WebDriver instance.
        /// </summary>
        private static void DisposeWebDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        /// <summary>
        /// Raises the DownloadProgress event.
        /// </summary>
        /// <param name="pageIndex">Index of the page being downloaded.</param>
        /// <param name="progressPercentage">Percentage of completion for the download operation.</param>
        private static void OnDownloadProgress(int pageIndex, double progressPercentage)
        {
            DownloadProgress?.Invoke(null, new DownloadProgressEventArgs
            {
                PageIndex = pageIndex,
                ProgressPercentage = progressPercentage
            });
        }
    }
}
