using HtmlAgilityPack;

namespace SupermarketInfo
{
    public class Tesco
    {
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
