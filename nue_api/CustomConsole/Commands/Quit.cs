namespace nue.CustomConsole.Commands
{
    public class Quit : ICommand
    {
        public Quit()
        {
        }

        public void Execute()
        {
            Logger.Instance.Log(LogType.Warning, $"Shutting down...");
            Environment.Exit(0);
        }
    }
}
