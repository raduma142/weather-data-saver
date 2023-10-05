using Microsoft.Extensions.Logging;
using System.Windows;
using WeatherDataSaver.Services.DataBaseService;
using WeatherDataSaver.Services.FileService;
using WeatherDataSaver.Services.ReportService;
using WeatherDataSaver.ViewModels;

namespace WeatherDataSaver
{
    public partial class MainWindow : Window
    {
        ILogger<MainWindow> _logger;
        MainWindowViewModel viewModel;
        public MainWindow(IReportCreater reportCreater, IFileAccess fileAccess, IDataBaseAccess dataBaseAccess, ILogger<MainWindow> logger)
        {
            InitializeComponent();
            viewModel = (MainWindowViewModel) DataContext;
            viewModel.dataBaseAccess = dataBaseAccess;
            viewModel.reportCreater = reportCreater;
            viewModel.fileAccess = fileAccess;
            _logger = logger;
            _logger.LogInformation("Запуск программы");
        }
    }
}
