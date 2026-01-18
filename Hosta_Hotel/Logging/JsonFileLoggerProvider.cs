using Microsoft.Extensions.Logging;

namespace Hosta_Hotel.Logging;

public class JsonFileLoggerProvider : ILoggerProvider
{
    private readonly string _path;

    public JsonFileLoggerProvider(string path)
    {
        _path = path;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new JsonFileLogger(_path);
    }

    public void Dispose() { }
}