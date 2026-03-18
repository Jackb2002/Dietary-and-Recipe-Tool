// PageDownloader has been moved to DietaryApp.Core (WinFormsInfoApp.Http.PageDownloader)
// so it can be tested cross-platform without a Windows build dependency.
// This file is kept as a thin re-export for any existing callers inside this assembly.

using WinFormsInfoApp.Http;

namespace SupermarketInfo
{
    /// <summary>
    /// Thin wrapper kept for backwards compatibility.
    /// Prefer using <see cref="WinFormsInfoApp.Http.PageDownloader"/> directly.
    /// </summary>
    public static class PageDownloader
    {
        private static readonly WinFormsInfoApp.Http.PageDownloader _inner = new();

        public static string DownloadPage(string pageUrl) => _inner.DownloadPage(pageUrl);

        public static string[] DownloadPages(string[] pageUrls, double delaySeconds = 0.5)
            => _inner.DownloadPages(pageUrls, delaySeconds);
    }
}
