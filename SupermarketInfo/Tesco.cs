using HtmlAgilityPack;
using System.Collections.Generic;

namespace SupermarketInfo
{
    public class Tesco
    {
        public static List<FoodItem> DownloadTescoURLs(string[] urls)
        {
            string[] page_1s = new string[urls.Length];
            for (int i = 0; i < urls.Length; i++)
            {
                string url = urls[i];
                var page_1 = url.Replace("PAGENUMBER", "1");
                page_1s[i] = page_1;
            }
            var page_1_htmls = PageDownloader.DownloadPages(page_1s);
            return null;
        }

        public static int GetMaxItems(string html)
        {
            // Load the HTML content into an HtmlDocument
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Find the element with the class 'pagination__items-displayed'
            HtmlNode paginationNode = doc.DocumentNode.SelectSingleNode("//div[@class='pagination__items-displayed']");

            if (paginationNode != null)
            {
                // Get the inner text of the element
                string innerText = paginationNode.InnerText;

                // Find the index of the closing strong tag after the second <strong> tag
                int startIndex = innerText.IndexOf("</strong>", innerText.IndexOf("<strong>") + 1);

                // Extract the substring containing the maximum amount of items
                string maxItemsText = innerText.Substring(startIndex + "</strong> of <strong>".Length);

                // Parse the maximum amount of items as an integer
                int maxItems = int.Parse(maxItemsText);

                // Return the maximum amount of items
                return maxItems;
            }
            else
            {
                // If the element is not found, return a default value
                return 0;
            }
        }
    }
}
