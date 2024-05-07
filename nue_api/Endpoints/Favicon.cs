using nue.HttpServer;
using System.Net;

namespace nue.Endpoints
{
    public class Favicon : IApiEndpoint
    {
        public string Path { get; } = "/favicon.ico";

        public async Task Handle(HttpListenerContext context)
        {
            byte[] faviconData = Properties.Resources.favicon; // Get favicon data in bytes.

            // Set response headers.
            context.Response.ContentType = "image/x-icon";
            context.Response.ContentLength64 = faviconData.Length;

            await context.Response.OutputStream.WriteAsync(faviconData, 0, faviconData.Length); // Write the favicon data to the response stream.
            context.Response.Close(); // Close response.
        }
    }
}
