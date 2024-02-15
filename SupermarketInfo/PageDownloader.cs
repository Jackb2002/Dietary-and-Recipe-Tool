using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;

namespace SupermarketInfo
{
    // Add comments to class and methods    // Add comments to class and methods
    /// <summary>
    /// The PageDownloader class provides methods to download
    /// web pages using the Selenium WebDriver.
    /// </summary>
    public static class PageDownloader
    {
        /// <summary>
        /// Downloads the HTML source of the given web page.
        /// </summary>
        /// <param name="page_url">The URL of the web page to download.</param>
        /// <returns>The HTML source of the web page.</returns>
        public static string DownloadPage(string page_url)
        {
            IWebDriver driver;
            try
            {
                driver = new ChromeDriver("chromedriver.exe");

            }
            catch (InvalidOperationException error)
            {
                Debug.WriteLine("ChromeDriver is not found or incorrect version.");
                Debug.WriteLine(error.Message);
                return string.Empty;
            }
            driver.Navigate().GoToUrl(page_url);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("return document.readyState").Equals("complete");
            string htmlSource = driver.PageSource;
            driver.Quit();
            return htmlSource;
        }
        /// <summary>
        /// Downloads the HTML source of the given web pages.
        /// </summary>
        /// <param name="page_urls">The URLs of the web pages to download.</param>
        /// <returns>The HTML source of the web pages.</returns>
        public static string[] DownloadPages(string[] page_urls)
        {
            IWebDriver driver;
            try
            {
                driver = new ChromeDriver("chromedriver.exe");
            }
            catch (InvalidOperationException error)
            {
                Debug.WriteLine("ChromeDriver is not found or incorrect version.");
                Debug.WriteLine(error.Message);
                return new string[0];
            }
            string[] html_sources = new string[page_urls.Length];
            for (int url_id = 0; url_id < page_urls.Length; url_id++)
            {
                string url = page_urls[url_id];
                driver.Navigate().GoToUrl(url);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("return document.readyState").Equals("complete");
                html_sources[url_id] = driver.PageSource;
            }
            driver.Quit();
            return html_sources;
        }
    }
}
