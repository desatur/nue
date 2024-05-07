using nue.CustomConsole;
using nue.Endpoints;
using nue.HttpServer;

namespace nue
{
    class Program
    {
        static void Main(string[] args)
        {
            Splash.Splash _ = new();
            Server instance = new();
            instance.Start();
            HelloWorld hello = new();
            Favicon favicon = new();
            Upload upload = new();
            instance.RegisterEndpoint(hello);
            instance.RegisterEndpoint(favicon);
            instance.RegisterEndpoint(upload);

            CommandSystem commandSystem = new CommandSystem();
            while (true)
            {
                string commandInput = Console.ReadLine();
                commandSystem.ProcessCommand(commandInput);
            }
        }
    }
}