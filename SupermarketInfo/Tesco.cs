using HtmlAgilityPack;
using System.Collections.Generic;
using System.Diagnostics;

namespace SupermarketInfo
{
    public class Tesco
    {
        /// <summary>
        /// Downloads and parses Tesco product pages.
        /// </summary>
        /// <param name="urls">
        /// URLs to fetch. Include the literal string "PAGENUMBER" where the page number should go.
        /// </param>
        /// <returns>List of parsed FoodItem results.</returns>
        public static List<FoodItem> DownloadTescoURLs(string[] urls)
        {
            string[] page1Urls = new string[urls.Length];
            for (int i = 0; i < urls.Length; i++)
                page1Urls[i] = urls[i].Replace("PAGENUMBER", "1");

            string[] htmlPages = PageDownloader.DownloadPages(page1Urls, delaySeconds: 1.5);

            var items = new List<FoodItem>();
            for (int i = 0; i < htmlPages.Length; i++)
            {
                if (string.IsNullOrEmpty(htmlPages[i]))
                {
                    Debug.WriteLine($"No HTML received for URL: {page1Urls[i]}");
                    continue;
                }
                int maxItems = GetMaxItems(htmlPages[i]);
                Debug.WriteLine($"URL {page1Urls[i]} reports {maxItems} items");
            }
            return items;
        }

        /// <summary>
        /// Parses the total result count from a Tesco search results page.
        /// </summary>
        /// <param name="html">Raw HTML of the results page.</param>
        /// <returns>Total item count, or 0 if the pagination element is not found.</returns>
        public static int GetMaxItems(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode? paginationNode = doc.DocumentNode
                .SelectSingleNode("//div[@class='pagination__items-displayed']");

            if (paginationNode == null)
                return 0;

            string innerText = paginationNode.InnerText;
            int secondStrong = innerText.IndexOf("</strong>", innerText.IndexOf("<strong>") + 1);
            if (secondStrong < 0)
                return 0;

            string maxItemsText = innerText.Substring(secondStrong + "</strong> of <strong>".Length);
            return int.TryParse(maxItemsText, out int maxItems) ? maxItems : 0;
        }
    }
}
