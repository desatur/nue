using nue.CustomConsole.Commands;

namespace nue.CustomConsole
{
    public class CommandSystem
    {
        private Dictionary<string, ICommand> commands;
        private Logger Logger;

        public CommandSystem()
        {
            commands = new Dictionary<string, ICommand>
            {
                { "quit", new Quit() },
                // Add more commands here if needed
            };
            Logger = new Logger();
        }

        public void ProcessCommand(string commandInput)
        {
            if (commands.TryGetValue(commandInput, out ICommand command))
            {
                command.Execute();
            }
            else
            {
                Logger.Log(LogType.Error, $"Command not recognized.");
            }
        }
    }
}
