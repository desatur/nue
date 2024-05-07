using nue.HttpServer;
using System.Net;

namespace nue.Endpoints
{
    internal class HelloWorld : IApiEndpoint
    {
        public string Path { get; } = "/hello";

        public async Task Handle(HttpListenerContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            using (StreamWriter writer = new StreamWriter(context.Response.OutputStream))
            {
                await writer.WriteAsync("Hello World!");
            }
            context.Response.Close();
        }
    }
}
