namespace CodeCool.SeasonalProductDiscounter.Service.Logger;

public class FileLogger : LoggerBase
{
    private readonly string _logFile;

    public FileLogger(string logFile)
    {
        _logFile = logFile;
    }

    protected override void LogMessage(string message, string type)
    {
        var entry = CreateLogEntry(message, type);
        using var streamWriter = File.AppendText(_logFile);
        streamWriter.WriteLine(entry);
        streamWriter.Close();
    }
}
