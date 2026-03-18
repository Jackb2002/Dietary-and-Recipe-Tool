using System.Net;
using System.Net.Http;
using WinFormsInfoApp.Http;

namespace UnitTests
{
    /// <summary>
    /// Stub HttpMessageHandler that returns a pre-configured response for any request.
    /// </summary>
    internal class StubHttpHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string _body;
        public List<HttpRequestMessage> Requests { get; } = [];

        public StubHttpHandler(HttpStatusCode statusCode = HttpStatusCode.OK, string body = "<html/>")
        {
            _statusCode = statusCode;
            _body = body;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add(request);
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_body)
            };
            return Task.FromResult(response);
        }
    }

    [TestClass]
    public class PageDownloaderTests
    {
        [TestMethod]
        public void DownloadPage_Returns_Body_On_200()
        {
            const string expected = "<html><body>Hello Tesco</body></html>";
            var handler = new StubHttpHandler(HttpStatusCode.OK, expected);
            var downloader = new PageDownloader(new HttpClient(handler));

            string result = downloader.DownloadPage("https://example.com/");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DownloadPage_Returns_EmptyString_On_404()
        {
            var handler = new StubHttpHandler(HttpStatusCode.NotFound);
            var downloader = new PageDownloader(new HttpClient(handler));

            string result = downloader.DownloadPage("https://example.com/missing");

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void DownloadPage_Returns_EmptyString_On_500()
        {
            var handler = new StubHttpHandler(HttpStatusCode.InternalServerError);
            var downloader = new PageDownloader(new HttpClient(handler));

            string result = downloader.DownloadPage("https://example.com/");

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void DownloadPage_Returns_EmptyString_When_Request_Throws()
        {
            // FaultingHandler throws to simulate a network error
            var downloader = new PageDownloader(new HttpClient(new FaultingHandler()));

            string result = downloader.DownloadPage("https://unreachable.invalid/");

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void DownloadPages_Returns_Array_Of_Correct_Length()
        {
            var handler = new StubHttpHandler(HttpStatusCode.OK, "<html/>");
            var downloader = new PageDownloader(new HttpClient(handler));

            string[] results = downloader.DownloadPages(["https://a.com/", "https://b.com/"], delaySeconds: 0);

            Assert.AreEqual(2, results.Length);
        }

        [TestMethod]
        public void DownloadPages_Issues_One_Request_Per_URL()
        {
            var handler = new StubHttpHandler();
            var downloader = new PageDownloader(new HttpClient(handler));
            string[] urls = ["https://a.com/", "https://b.com/", "https://c.com/"];

            downloader.DownloadPages(urls, delaySeconds: 0);

            Assert.AreEqual(3, handler.Requests.Count);
        }

        [TestMethod]
        public void DownloadPages_Requests_Correct_URLs()
        {
            var handler = new StubHttpHandler();
            var downloader = new PageDownloader(new HttpClient(handler));
            string[] urls = ["https://first.com/", "https://second.com/"];

            downloader.DownloadPages(urls, delaySeconds: 0);

            Assert.AreEqual("https://first.com/", handler.Requests[0].RequestUri!.ToString());
            Assert.AreEqual("https://second.com/", handler.Requests[1].RequestUri!.ToString());
        }

        [TestMethod]
        public void DownloadPages_Partial_Failure_Still_Returns_Full_Array()
        {
            // First URL succeeds, second fails
            var handler = new MixedResponseHandler(
                [HttpStatusCode.OK, HttpStatusCode.ServiceUnavailable],
                ["<html>page1</html>", ""]);
            var downloader = new PageDownloader(new HttpClient(handler));

            string[] results = downloader.DownloadPages(
                ["https://a.com/", "https://b.com/"], delaySeconds: 0);

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("<html>page1</html>", results[0]);
            Assert.AreEqual(string.Empty, results[1]);
        }
    }

    [TestClass]
    public class PageDownloaderIntegrationTests
    {
        // Run with: dotnet test --filter "TestCategory=Integration"
        // Skip in CI with: dotnet test --filter "TestCategory!=Integration"

        [TestMethod]
        [TestCategory("Integration")]
        public void DownloadPage_Fetches_Real_Page_Over_HTTP()
        {
            // Tesco requires cookie consent + JS execution, so it returns 403 to plain
            // HttpClient requests — that is expected and documented in the test below.
            // example.com is the IANA reserved test domain: always available, always returns HTML.
            const string url = "https://example.com/";
            var downloader = new PageDownloader();

            string html = downloader.DownloadPage(url);

            Assert.IsFalse(string.IsNullOrWhiteSpace(html),
                "Expected non-empty HTML from example.com.");
            StringAssert.Contains(html.ToLower(), "<html",
                "Response should be an HTML document.");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void DownloadPage_Returns_EmptyString_For_Tesco_Bot_Detection()
        {
            // Documents the known limitation: Tesco serves a 403 to non-browser clients.
            // A future improvement would use a headless browser or Playwright.
            const string url = "https://www.tesco.com/groceries/en-GB/products/254868438";
            var downloader = new PageDownloader();

            string html = downloader.DownloadPage(url);

            Assert.AreEqual(string.Empty, html,
                "Tesco is expected to block plain HttpClient requests with a 403.");
        }
    }

    /// <summary>Handler that throws HttpRequestException to simulate a network failure.</summary>
    internal class FaultingHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
            => throw new HttpRequestException("Simulated network failure");
    }

    /// <summary>Handler that returns a different status code per request in sequence.</summary>
    internal class MixedResponseHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode[] _codes;
        private readonly string[] _bodies;
        private int _index;

        public MixedResponseHandler(HttpStatusCode[] codes, string[] bodies)
        {
            _codes = codes;
            _bodies = bodies;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            int i = _index++;
            var response = new HttpResponseMessage(_codes[i])
            {
                Content = new StringContent(_bodies[i])
            };
            return Task.FromResult(response);
        }
    }
}
