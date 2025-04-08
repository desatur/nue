namespace nue.CustomConsole
{
    public class Logger
    {
        public static readonly Logger Instance = new();
        private string logFilePath;

        public Logger()
        {
            string logsDirectory = "logs";
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            // Create log file with the current date
            string dateStamp = DateTime.Now.ToString("yyyyMMdd");
            logFilePath = Path.Combine(logsDirectory, $"{dateStamp}.txt");
        }

        public void Log(LogType type, string message, string? codeContext = null)
        {
            string logMessage = $"[{DateTime.Now}] [{type.ToString().ToUpper()}]";

            if (!string.IsNullOrEmpty(codeContext))
                logMessage += $" [{codeContext}]";

            logMessage += $" {message}";

            Console.WriteLine(logMessage);
            WriteLogToFile(logMessage);
        }

        private void WriteLogToFile(string logMessage)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}