using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using WeatherDataSaver.Services.ReportService;
using WeatherDataSaver.ViewModels;
using WeatherDataSaver.Services.FileService;
using WeatherDataSaver.Services.DataBaseService;

namespace WeatherDataSaver
{
    internal class Program
    {
        public static IHost host { get; private set; }
        [STAThread]
        public static void Main()
        {
            var hostApplicationBuilder = Host.CreateDefaultBuilder();
            host = hostApplicationBuilder
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddTransient<IFileAccess, FileAccess>();
                    services.AddTransient<IReportCreater, ReportCreater>();
                    services.AddTransient<IDataBaseAccess, DataBaseAccess>();
                    services.AddLogging();
                }).Build();

            var app = host.Services.GetService<App>();
            app.Run();
        }
    }
}
