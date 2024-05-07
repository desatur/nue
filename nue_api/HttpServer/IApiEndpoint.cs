using System.Net;

namespace nue.HttpServer
{
    public interface IApiEndpoint
    {
        string Path { get; }
        Task Handle(HttpListenerContext context);
    }
}
