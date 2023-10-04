using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WeatherDataSaver
{
    internal class Program
    {
        public static IHost programHost { get; private set; }
        [STAThread]
        public static void Main()
        {
            IHostBuilder hostApplicationBuilder = Host.CreateDefaultBuilder();
            programHost = hostApplicationBuilder
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddLogging();
                }).Build();
            App app = programHost.Services.GetService<App>();
            app.Run();
        }
    }
}
