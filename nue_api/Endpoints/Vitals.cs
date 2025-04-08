using System.Net;
using Newtonsoft.Json;
using nue.HttpServer;
using static nue.HttpServer.Endpoint;

namespace nue.Endpoints
{
    internal class Vitals : IApiEndpoint
    {
        public string Path { get; } = "/vitals";

        public async Task Handle(HttpListenerContext context)
        {
            var obj = new
            {
                bpm = 250,
                error = "This is not implemented."
            };
            await Respond(ResponseType.API, JsonConvert.SerializeObject(obj), context);
        }
    }
}
