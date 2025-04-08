using System.Reflection;

namespace nue.Splash
{
    internal static class Splash
    {
        internal static void Display()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" ▄▀▀▄ ▀▄  ▄▀▀▄ ▄▀▀▄  ▄▀▀█▄▄▄▄ \r\n█  █ █ █ █   █    █ ▐  ▄▀   ▐ \r\n▐  █  ▀█ ▐  █    █    █▄▄▄▄▄  \r\n  █   █    █    █     █    ▌  \r\n▄▀   █      ▀▄▄▄▄▀   ▄▀▄▄▄▄   \r\n█    ▐               █    ▐   \r\n▐                    ▐        API ver. {version}");
            Console.Title = "nue";
        }
    }
}
