using System.Diagnostics;
using HtmlAgilityPack;

namespace WinFormsInfoApp.Supermarket
{
    /// <summary>
    /// Fetches product pricing from Trolley.co.uk using HttpClient + HtmlAgilityPack.
    /// No API key required. Trolley aggregates prices from Tesco, Sainsbury's, ASDA,
    /// Morrisons, Waitrose, Ocado and others.
    /// </summary>
    public class TrolleyAPI
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://www.trolley.co.uk";

        public TrolleyAPI() : this(new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(15),
            DefaultRequestHeaders = { { "User-Agent", "Mozilla/5.0" } }
        })
        { }

        public TrolleyAPI(HttpClient client) => _client = client;

        /// <summary>
        /// Searches Trolley for products matching <paramref name="query"/>.
        /// Returns up to the first page of results (~20 items).
        /// </summary>
        public List<ProductPrice> SearchProducts(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return [];

            string url = $"{BaseUrl}/search/?q={Uri.EscapeDataString(query)}";
            try
            {
                string html = Fetch(url);
                if (string.IsNullOrEmpty(html)) return [];
                return ParseSearchResults(html);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"TrolleyAPI.SearchProducts error: {ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// Returns per-store prices for a specific Trolley product ID.
        /// </summary>
        public List<ProductPrice> GetProductPrices(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId)) return [];

            string url = $"{BaseUrl}/product/-/{productId}";
            try
            {
                string html = Fetch(url);
                if (string.IsNullOrEmpty(html)) return [];
                return ParseProductPrices(html, productId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"TrolleyAPI.GetProductPrices error: {ex.Message}");
                return [];
            }
        }

        // ── private helpers ────────────────────────────────────────────────────

        private string Fetch(string url)
        {
            HttpResponseMessage response = _client.GetAsync(url).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"TrolleyAPI HTTP {(int)response.StatusCode} for {url}");
                return string.Empty;
            }
            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public static List<ProductPrice> ParseSearchResults(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var items = doc.DocumentNode
                .SelectNodes("//div[contains(@class,'product-item')][@data-id]");

            if (items == null) return [];

            var results = new List<ProductPrice>();
            foreach (var item in items)
            {
                string productId = item.GetAttributeValue("data-id", string.Empty);

                var link = item.SelectSingleNode(".//a[@href and @title]");
                if (link == null) continue;

                string name = link.GetAttributeValue("title", string.Empty);
                string href = link.GetAttributeValue("href", string.Empty);

                var priceNode = item.SelectSingleNode(".//*[contains(@class,'_price')]");
                var perUnitNode = item.SelectSingleNode(".//*[contains(@class,'_per-item')]");

                // Price is a raw text node inside _price (not wrapped in a child element).
                // Extract only direct text nodes to avoid picking up the per-unit child.
                string rawPrice = priceNode?
                    .ChildNodes
                    .Where(n => n.NodeType == HtmlAgilityPack.HtmlNodeType.Text)
                    .Select(n => n.InnerText.Trim())
                    .FirstOrDefault(t => t.Contains("&pound;") || t.Contains("£") || t.Any(char.IsDigit))
                    ?? string.Empty;
                string perUnit = perUnitNode?.InnerText?.Trim() ?? string.Empty;
                decimal price = ParsePound(rawPrice);

                results.Add(new ProductPrice
                {
                    Name = HtmlEntity.DeEntitize(name).Trim(),
                    ProductId = productId,
                    Url = href,
                    Price = price,
                    PricePerUnit = HtmlEntity.DeEntitize(perUnit).Trim()
                });
            }
            return results;
        }

        public static List<ProductPrice> ParseProductPrices(string html, string productId)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Each store entry lives in a <div class="_item"> with an SVG store logo
            var storeItems = doc.DocumentNode.SelectNodes("//div[contains(@class,'_item')]");
            if (storeItems == null) return [];

            var results = new List<ProductPrice>();
            foreach (var storeItem in storeItems)
            {
                // Store name comes from the <svg title="Tesco"> or <title> child of the svg
                var svgTitle = storeItem.SelectSingleNode(".//svg/@title") ??
                               storeItem.SelectSingleNode(".//svg/title");
                string store = svgTitle?.GetAttributeValue("title", null)
                               ?? svgTitle?.InnerText?.Trim()
                               ?? string.Empty;

                if (string.IsNullOrEmpty(store)) continue;

                var boldPrice = storeItem.SelectSingleNode(".//*[contains(@class,'_price')]//b");
                var perUnitNode = storeItem.SelectSingleNode(".//*[contains(@class,'_per-item')]");

                if (boldPrice == null) continue;

                decimal price = ParsePound(boldPrice.InnerText);
                string perUnit = perUnitNode?.InnerText?.Trim() ?? string.Empty;

                results.Add(new ProductPrice
                {
                    ProductId = productId,
                    Store = store,
                    Price = price,
                    PricePerUnit = HtmlEntity.DeEntitize(perUnit).Trim()
                });
            }
            return results;
        }

        private static decimal ParsePound(string text)
        {
            // Strip HTML entities, £ sign, whitespace and parse
            string clean = HtmlEntity.DeEntitize(text ?? string.Empty)
                .Replace("£", string.Empty)
                .Split('\n')[0]
                .Trim();
            return decimal.TryParse(clean, out decimal val) ? val : 0m;
        }
    }
}
