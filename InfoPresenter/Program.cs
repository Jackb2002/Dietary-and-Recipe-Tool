internal class Program
{
    private static void Main(string[] args)
    {
        string html = SupermarketInfo.WebTools.GetPageHTML(@"https://www.tesco.com/groceries/en-GB/shop/fresh-food/fresh-fruit/bananas");
        Console.WriteLine(html);
        Console.ReadKey();
    }
}