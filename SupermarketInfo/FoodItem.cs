namespace SupermarketInfo
{
    public class FoodItem
    {
        public string Name { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string Store { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PricePerUnit { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
