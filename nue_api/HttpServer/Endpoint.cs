using System.Net;

namespace nue.HttpServer
{
    public static class Endpoint
    {
        public enum ResponseType : byte
        {
            None,
            API
        }

        public async static Task Respond(ResponseType responseType, string data, HttpListenerContext context)
        {
            switch (responseType)
            {
                case ResponseType.None:
                    break;
                case ResponseType.API:
                    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    context.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                    context.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
                    break;
                default:
                    break;
            }
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            using (StreamWriter writer = new(context.Response.OutputStream))
            {
                await writer.WriteAsync(data);
            }
            context.Response.Close();
        }
    }
}
