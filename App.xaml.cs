using Microsoft.Extensions.Hosting;
using System.Windows;

namespace WeatherDataSaver
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {

            }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            base.OnStartup(e);
        }
    }
}
