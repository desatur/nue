using nue.CustomConsole;
using nue.Endpoints;
using nue.HttpServer;

namespace nue
{
    class Program
    {
        private static List<IApiEndpoint> DefaultEndpoints;

        static void Main(/*string[] args*/)
        {
            Splash.Splash.Display();
            Logger.Instance.Log(LogType.Info, "Initializing classes of Default Endpoints");
            DefaultEndpoints = new ()
            {
                { new HelloWorld() },
                { new Favicon() },
                { new Vitals() },
            };
            Server.Instance.Start();
            Logger.Instance.Log(LogType.Info, "Registering Default Endpoints");
            foreach (var endpoint in DefaultEndpoints)
            {
                Server.Instance.RegisterEndpoint(endpoint);
            }
            Logger.Instance.Log(LogType.Info, "Everything is done I guess?");
            while (true)
            {
                CommandSystem.Instance.ProcessCommand(Console.ReadLine());
            }
        }
    }
}