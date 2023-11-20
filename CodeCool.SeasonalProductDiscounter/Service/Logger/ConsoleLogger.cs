namespace CodeCool.SeasonalProductDiscounter.Service.Logger;

public class ConsoleLogger : LoggerBase
{
    protected override void LogMessage(string message, string type)
    {
        var entry = CreateLogEntry(message, type);
        Console.WriteLine(entry);
    }
}
