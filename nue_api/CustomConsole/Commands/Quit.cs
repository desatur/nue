namespace nue.CustomConsole.Commands
{
    public class Quit : ICommand
    {
        private readonly Logger Logger;

        public Quit()
        {
            Logger = new Logger();
        }

        public void Execute()
        {
            Logger.Log(LogType.Warning, $"Shutting down...");
            Environment.Exit(0);
        }
    }
}
