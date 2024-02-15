internal class Program
{
    private static void Main(string[] args)
    {
        string html = SupermarketInfo.PageDownloader.DownloadPage(@"https://www.tesco.com/groceries/en-GB/shop/fresh-food/fresh-fruit/bananas");
        Console.WriteLine(html);
        Console.ReadKey();
    }
}