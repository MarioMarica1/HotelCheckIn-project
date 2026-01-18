using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Hosta_Hotel.Logging;

public class JsonFileLogger : ILogger
{
    private readonly string _filePath;
    private static readonly object _fileLock = new object(); // e un token, vezi linia 36

    public JsonFileLogger(string path)
    {
        _filePath = path;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (formatter == null) return;

        // Obiect anonim pentru serializare
        var logEntry = new
        {
            Timestamp = DateTime.Now,
            Level = logLevel.ToString(),
            EventId = eventId.Id,
            Message = formatter(state, exception),
            Exception = exception?.Message
        };

        string jsonLine = JsonSerializer.Serialize(logEntry);

        // Compilatorul traduce asta ca fiind un "try-finally"
        // in clasa Monitor (Monitor.Enter/Exit)
        lock (_fileLock)
        {
            File.AppendAllText(_filePath, jsonLine + Environment.NewLine);
        }
    }
}