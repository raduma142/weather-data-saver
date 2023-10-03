using Microsoft.Extensions.Logging;
using System.Windows;

namespace WeatherDataSaver
{
    public partial class MainWindow : Window
    {
        public MainWindow(ILogger<MainWindow> logger)
        {
            InitializeComponent();
        }
    }
}
