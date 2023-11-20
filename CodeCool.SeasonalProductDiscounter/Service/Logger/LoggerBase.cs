namespace CodeCool.SeasonalProductDiscounter.Service.Logger;

public abstract class LoggerBase : ILogger
{
    public void LogInfo(string message)
    {
        LogMessage(message, "INFO");
    }

    public void LogError(string message)
    {
        LogMessage(message, "ERROR");
    }

    protected static string CreateLogEntry(string message, string type) => $"[{DateTime.Now}] {type}: {message}";

    protected abstract void LogMessage(string message, string type);
}
