using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Hosta_Hotel.TheBrain;
using Hosta_Hotel.Infrastructure;
using Hosta_Hotel.Logging;

namespace Hosta_Hotel;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<PersistenceManager>();
                services.AddSingleton<Hotel>();
                services.AddHostedService<HotelApp>();
                services.AddLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddProvider(new JsonFileLoggerProvider("hotel_logs.json"));
                    builder.SetMinimumLevel(LogLevel.Information);
                });
            })
            .Build();

        await host.RunAsync();
    }
}