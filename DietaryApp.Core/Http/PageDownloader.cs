using System.Diagnostics;

namespace WinFormsInfoApp.Http
{
    /// <summary>
    /// Downloads web pages using HttpClient with a realistic browser User-Agent.
    /// Accepts an injected HttpClient so the caller (or tests) can supply a custom handler.
    /// </summary>
    public class PageDownloader
    {
        private readonly HttpClient _client;

        private const string DefaultUserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/124.0.0.0 Safari/537.36";

        /// <summary>
        /// Creates a PageDownloader with a default HttpClient (browser-like headers, 15s timeout).
        /// </summary>
        public PageDownloader() : this(CreateDefaultClient()) { }

        /// <summary>
        /// Creates a PageDownloader with the supplied HttpClient.
        /// Use this overload in tests to inject a mock handler.
        /// </summary>
        public PageDownloader(HttpClient client)
        {
            _client = client;
        }

        private static HttpClient CreateDefaultClient()
        {
            var client = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
            client.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
            client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9");
            return client;
        }

        /// <summary>
        /// Downloads the HTML source of a single web page.
        /// </summary>
        /// <returns>HTML string, or empty string on any failure.</returns>
        public string DownloadPage(string pageUrl)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(pageUrl).Result;
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"HTTP {(int)response.StatusCode} fetching {pageUrl}");
                    return string.Empty;
                }
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to download {pageUrl}: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Downloads the HTML source of multiple pages with a polite delay between requests.
        /// </summary>
        /// <param name="pageUrls">URLs to fetch.</param>
        /// <param name="delaySeconds">Seconds to wait between each request.</param>
        /// <returns>Array of HTML strings; empty string where a fetch failed.</returns>
        public string[] DownloadPages(string[] pageUrls, double delaySeconds = 0.5)
        {
            string[] htmlSources = new string[pageUrls.Length];
            for (int i = 0; i < pageUrls.Length; i++)
            {
                htmlSources[i] = DownloadPage(pageUrls[i]);
                if (i < pageUrls.Length - 1 && delaySeconds > 0)
                    Thread.Sleep((int)(delaySeconds * 1000));
            }
            return htmlSources;
        }
    }
}
