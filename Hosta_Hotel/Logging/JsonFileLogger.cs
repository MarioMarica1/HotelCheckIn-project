using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Hosta_Hotel.Logging;

public class JsonFileLogger : ILogger
{
    private readonly string _filePath;
    private static readonly object _lock = new object();

    public JsonFileLogger(string path)
    {
        _filePath = path;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (formatter == null) return;

        var logEntry = new
        {
            Timestamp = DateTime.Now,
            Level = logLevel.ToString(),
            Message = formatter(state, exception),
            Exception = exception?.Message
        };

        string jsonLine = JsonSerializer.Serialize(logEntry);

        lock (_lock)
        {
            File.AppendAllText(_filePath, jsonLine + Environment.NewLine);
        }
    }
}