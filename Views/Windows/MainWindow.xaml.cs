using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WeatherDataSaver.Services.DataBaseService;
using WeatherDataSaver.Services.FileService;
using WeatherDataSaver.Services.ReportService;
using WeatherDataSaver.ViewModels;

namespace WeatherDataSaver
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;

        List<Key> Keys = new List<Key>()
        {
            Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
            Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4,
            Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9,
            Key.Separator, Key.OemPlus, Key.OemMinus, Key.Subtract, Key.Add,
            Key.Decimal, Key.OemPeriod
        };
        public MainWindow(IReportCreater reportCreater, IFileAccess fileAccess, IDataBaseAccess dataBaseAccess)
        {
            InitializeComponent();
            viewModel = (MainWindowViewModel) DataContext;
            viewModel.dataBaseAccess = dataBaseAccess;
            viewModel.reportCreater = reportCreater;
            viewModel.fileAccess = fileAccess;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = Keys.IndexOf(e.Key) == -1;
        }
    }
}
