using nue.CustomConsole.Commands;

namespace nue.CustomConsole
{
    public class CommandSystem
    {
        private Dictionary<string, ICommand> commands;
        public static readonly CommandSystem Instance = new();

        public CommandSystem()
        {
            commands = new Dictionary<string, ICommand>
            {
                { "quit", new Quit() },
            };
            Logger.Instance.Log(LogType.Info, "CommandSystem started");
        }

        public void ProcessCommand(string commandInput)
        {
            if (commands.TryGetValue(commandInput, out ICommand command))
            {
                command.Execute();
            }
            else
            {
                Logger.Instance.Log(LogType.Error, $"Command not recognized.");
            }
        }
    }
}
