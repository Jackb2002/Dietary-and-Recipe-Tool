using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using WinFormsInfoApp.Supermarket;

namespace UnitTests
{
    // ── Unit tests (no network) ────────────────────────────────────────────────

    [TestClass]
    public class TrolleyAPITests
    {
        // Minimal Trolley search results page HTML
        private const string SearchHtml = """
            <html><body>
              <div class="product-item " data-id="IRL393" ruid="IIH627">
                <a href="/product/quaker-rolled-porridge-oats/IRL393"
                   title="Quaker Rolled Porridge Oats (1kg)">
                  <div class="_price">&pound;2.75
                    <div class="_per-item">&pound;0.28 per 100g</div>
                  </div>
                </a>
              </div>
              <div class="product-item " data-id="IXU514" ruid="ABC123">
                <a href="/product/quaker-jumbo-oats/IXU514"
                   title="Quaker Jumbo Porridge Oats (750g)">
                  <div class="_price">&pound;2.95
                    <div class="_per-item">&pound;0.39 per 100g</div>
                  </div>
                </a>
              </div>
            </body></html>
            """;

        // Minimal Trolley product page HTML with two store entries
        private const string ProductHtml = """
            <html><body>
              <div class="_item">
                <div>
                  <svg title="Tesco" class="store-logo -tesco">
                    <title>Tesco</title>
                  </svg>
                </div>
                <div>
                  <div class="_price"><b>&pound;2.75</b>
                    <div class="_per-item -color-grey">&pound;0.28 per 100g</div>
                  </div>
                </div>
              </div>
              <div class="_item">
                <div>
                  <svg title="Waitrose" class="store-logo -waitrose">
                    <title>Waitrose</title>
                  </svg>
                </div>
                <div>
                  <div class="_price"><b>&pound;3.15</b>
                    <div class="_per-item -color-grey">&pound;0.32 per 100g</div>
                  </div>
                </div>
              </div>
            </body></html>
            """;

        // ── ParseSearchResults ─────────────────────────────────────────────────

        [TestMethod]
        public void ParseSearchResults_Returns_Correct_Count()
        {
            var results = TrolleyAPI.ParseSearchResults(SearchHtml);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void ParseSearchResults_Maps_Name_And_Id()
        {
            var result = TrolleyAPI.ParseSearchResults(SearchHtml)[0];
            Assert.AreEqual("Quaker Rolled Porridge Oats (1kg)", result.Name);
            Assert.AreEqual("IRL393", result.ProductId);
        }

        [TestMethod]
        public void ParseSearchResults_Maps_Price()
        {
            var result = TrolleyAPI.ParseSearchResults(SearchHtml)[0];
            Assert.AreEqual(2.75m, result.Price);
        }

        [TestMethod]
        public void ParseSearchResults_Maps_PricePerUnit()
        {
            var result = TrolleyAPI.ParseSearchResults(SearchHtml)[0];
            StringAssert.Contains(result.PricePerUnit, "0.28 per 100g");
        }

        [TestMethod]
        public void ParseSearchResults_Maps_Url()
        {
            var result = TrolleyAPI.ParseSearchResults(SearchHtml)[0];
            Assert.AreEqual("/product/quaker-rolled-porridge-oats/IRL393", result.Url);
        }

        [TestMethod]
        public void ParseSearchResults_Returns_Empty_For_Empty_Html()
        {
            Assert.AreEqual(0, TrolleyAPI.ParseSearchResults(string.Empty).Count);
        }

        // ── ParseProductPrices ─────────────────────────────────────────────────

        [TestMethod]
        public void ParseProductPrices_Returns_Correct_Count()
        {
            var results = TrolleyAPI.ParseProductPrices(ProductHtml, "IRL393");
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void ParseProductPrices_Maps_Store_Names()
        {
            var results = TrolleyAPI.ParseProductPrices(ProductHtml, "IRL393");
            Assert.AreEqual("Tesco", results[0].Store);
            Assert.AreEqual("Waitrose", results[1].Store);
        }

        [TestMethod]
        public void ParseProductPrices_Maps_Prices()
        {
            var results = TrolleyAPI.ParseProductPrices(ProductHtml, "IRL393");
            Assert.AreEqual(2.75m, results[0].Price);
            Assert.AreEqual(3.15m, results[1].Price);
        }

        [TestMethod]
        public void ParseProductPrices_Propagates_ProductId()
        {
            var results = TrolleyAPI.ParseProductPrices(ProductHtml, "IRL393");
            Assert.IsTrue(results.All(r => r.ProductId == "IRL393"));
        }

        [TestMethod]
        public void ParseProductPrices_Returns_Empty_For_Empty_Html()
        {
            Assert.AreEqual(0, TrolleyAPI.ParseProductPrices(string.Empty, "IRL393").Count);
        }

        // ── SearchProducts HTTP behaviour ──────────────────────────────────────

        [TestMethod]
        public void SearchProducts_Returns_Empty_On_HTTP_Error()
        {
            var handler = new StaticResponseHandler(string.Empty, HttpStatusCode.ServiceUnavailable);
            var api = new TrolleyAPI(new HttpClient(handler));
            Assert.AreEqual(0, api.SearchProducts("oats").Count);
        }

        [TestMethod]
        public void SearchProducts_Returns_Empty_For_Blank_Query()
        {
            var api = new TrolleyAPI(new HttpClient(new StaticResponseHandler(SearchHtml)));
            Assert.AreEqual(0, api.SearchProducts("   ").Count);
        }

        // ── helper ────────────────────────────────────────────────────────────

        private class StaticResponseHandler : HttpMessageHandler
        {
            private readonly string _body;
            private readonly HttpStatusCode _code;
            public StaticResponseHandler(string body,
                HttpStatusCode code = HttpStatusCode.OK)
            { _body = body; _code = code; }

            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage req, CancellationToken ct)
                => Task.FromResult(new HttpResponseMessage(_code)
                    { Content = new StringContent(_body) });
        }
    }

    // ── Integration tests (live network) ──────────────────────────────────────

    [TestClass]
    public class TrolleyAPIIntegrationTests
    {
        // Run with:   dotnet test --filter "TestCategory=Integration"
        // Skip in CI: dotnet test --filter "TestCategory!=Integration"

        [TestMethod]
        [TestCategory("Integration")]
        public void SearchProducts_Returns_Real_Results_For_Oats()
        {
            var api = new TrolleyAPI();
            var results = api.SearchProducts("oats");

            Assert.IsTrue(results.Count > 0, "Expected at least one result for 'oats'.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(results[0].Name));
            Assert.IsTrue(results[0].Price > 0m, "Expected a positive price.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(results[0].ProductId));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetProductPrices_Returns_Multiple_Stores_For_Known_Product()
        {
            var api = new TrolleyAPI();
            // IRL393 = Quaker Rolled Porridge Oats — stocked by several UK supermarkets
            var prices = api.GetProductPrices("IRL393");

            Assert.IsTrue(prices.Count > 1, "Expected prices from more than one store.");
            Assert.IsTrue(prices.All(p => p.Price > 0m), "All store prices should be positive.");
            Assert.IsTrue(prices.All(p => !string.IsNullOrWhiteSpace(p.Store)),
                "All entries should have a store name.");
        }
    }
}
