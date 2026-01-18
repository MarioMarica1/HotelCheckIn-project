using Microsoft.Extensions.Logging;

namespace Hosta_Hotel.Logging;

public class JsonFileLoggerProvider : ILoggerProvider
{
    private readonly string _path;

    public JsonFileLoggerProvider(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new Exception("Pathul nu poate fi gol.");
        }
        _path = path;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new JsonFileLogger(_path);
    }
    // Fisierele de inchid automat dupa scriere / suprascriere
    // Nu e nevoie de dispose
    public void Dispose() { }
}