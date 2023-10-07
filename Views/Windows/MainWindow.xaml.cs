using System.Windows;
using WeatherDataSaver.Services.DataBaseService;
using WeatherDataSaver.Services.FileService;
using WeatherDataSaver.Services.ReportService;
using WeatherDataSaver.ViewModels;

namespace WeatherDataSaver
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
        public MainWindow(IReportCreater reportCreater, IFileAccess fileAccess, IDataBaseAccess dataBaseAccess)
        {
            InitializeComponent();
            viewModel = (MainWindowViewModel) DataContext;
            viewModel.dataBaseAccess = dataBaseAccess;
            viewModel.reportCreater = reportCreater;
            viewModel.fileAccess = fileAccess;
        }
    }
}
