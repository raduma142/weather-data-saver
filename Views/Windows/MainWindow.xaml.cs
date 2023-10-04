using Microsoft.Extensions.Logging;
using System.Windows;
using WeatherDataSaver.Services.FileService;
using WeatherDataSaver.Services.ReportService;
using WeatherDataSaver.ViewModels;

namespace WeatherDataSaver
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
        public MainWindow(IReportCreater reportCreater, IFileAccess fileAccess)
        {
            InitializeComponent();
            viewModel = (MainWindowViewModel) DataContext;
            viewModel.reportCreater = reportCreater;
            viewModel.fileAccess = fileAccess;
        }
    }
}
