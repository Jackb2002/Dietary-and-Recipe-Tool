namespace WinFormsInfoApp.Supermarket
{
    /// <summary>
    /// A supermarket product with optional per-store pricing.
    /// Returned by <see cref="TrolleyAPI"/>.
    /// </summary>
    public class ProductPrice
    {
        /// <summary>Product display name.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Trolley product ID (e.g. "IRL393").</summary>
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// Supermarket name when returned from <see cref="TrolleyAPI.GetProductPrices"/>.
        /// Empty for search results.
        /// </summary>
        public string Store { get; set; } = string.Empty;

        /// <summary>Price in GBP.</summary>
        public decimal Price { get; set; }

        /// <summary>Unit price string as shown on Trolley (e.g. "£0.28 per 100g").</summary>
        public string PricePerUnit { get; set; } = string.Empty;

        /// <summary>Relative Trolley URL for the product page.</summary>
        public string Url { get; set; } = string.Empty;
    }
}
