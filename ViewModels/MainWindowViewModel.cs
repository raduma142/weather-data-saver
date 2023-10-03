using System;
using WeatherDataSaver.ViewModels.Base;

namespace WeatherDataSaver.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Properties

        //Temperature
        private float _temperature = 0f;
        public float temperature
        {
            get => _temperature;
            set => Set(ref _temperature, value);
        }

        //Condition
        private string _condition = "Ясно";
        public string condition
        {
            get => _condition;
            set => Set(ref _condition, value);
        }

        //Note
        private string _note = "";
        public string note
        {
            get => _note;
            set => Set(ref _note, value);
        }

        //Date
        private string _date = DateTime.Now.ToString("d");
        public string date
        {
            get => _date;
            set => Set(ref _date, value);
        }

        //Time
        private string _time = DateTime.Now.ToString("HH:mm:ss");
        public string time
        {
            get => _time;
            set => Set(ref _time, value);
        }
        #endregion
    }
}
