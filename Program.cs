using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using WeatherDataSaver.Services.ReportService;
using WeatherDataSaver.ViewModels;
using WeatherDataSaver.Services.FileService;
using WeatherDataSaver.Services.DataBaseService;
using Serilog;
using System.Data;
using Serilog.Events;

namespace WeatherDataSaver
{
    internal class Program
    {
        public static IHost host { get; private set; }
        [STAThread]
        public static void Main()
        {
            string logfile = string.Format("./logs/log {0}.txt", DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logfile)
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

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
                }).Build();

            Log.Information("Start Application");

            var app = host.Services.GetService<App>();
            app.Run();
        }
    }
}
