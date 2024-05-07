using System.Text;
using System.Net;
using nue.HttpServer;

namespace nue.Endpoints
{
    public class Upload : IApiEndpoint
    {
        public string Path => "/upload";

        public async Task Handle(HttpListenerContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                using (StreamReader reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    string requestData = await reader.ReadToEndAsync();
                    string responseString = "Upload successful!";
                    byte[] responseBytes = Encoding.UTF8.GetBytes(responseString);
                    context.Response.ContentType = "text/plain";
                    context.Response.ContentLength64 = responseBytes.Length;
                    await context.Response.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                await using StreamWriter writer = new StreamWriter(context.Response.OutputStream);
                await writer.WriteAsync("Method not allowed");
            }

            context.Response.Close();
        }
    }
}