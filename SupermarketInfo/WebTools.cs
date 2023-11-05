using AngleSharp;
using AngleSharp.Dom;

namespace SupermarketInfo
{
    public static class WebTools
    {
        public static string GetPageHTML(string url)
        {
            IDocument? doc = GetPageDocumentAsync(url).GetAwaiter().GetResult();
            if(doc == null)
            {
                return string.Empty;
            }
            string html = doc.DocumentElement.OuterHtml;
            return html;
        }

        static async Task<IDocument> GetPageDocumentAsync(string url)
        {
            using var httpClient = new HttpClient();

            // Set a 10-second timeout for the HTTP request
            httpClient.Timeout = TimeSpan.FromSeconds(10);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string pageContent = await response.Content.ReadAsStringAsync();

                    var context = BrowsingContext.New(Configuration.Default);
                    var document = await context.OpenAsync(req => req.Content(pageContent));

                    return document;
                }
                else
                {
                    throw new Exception($"HTTP request failed with status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error in HTTP request: " + ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception("Request timed out: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }
    }
}