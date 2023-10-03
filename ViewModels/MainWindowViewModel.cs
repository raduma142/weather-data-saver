using WeatherDataSaver.ViewModels.Base;

namespace WeatherDataSaver.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Properties
        private string _line = "This is MVVM string";
        public string line
        {
            get => _line;
            set => Set(ref _line, value);
        }
        #endregion
    }
}
