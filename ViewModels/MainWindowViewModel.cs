using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;
using WeatherDataSaver.Infrascructure.Commands;
using WeatherDataSaver.Models;
using WeatherDataSaver.Services.ReportService;
using WeatherDataSaver.ViewModels.Base;

namespace WeatherDataSaver.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Properties

        //DataSet
        public ObservableCollection<DataRecord> dataSet {  get; set; } = new ObservableCollection<DataRecord>();

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

        //Report
        private string _report = "Отчёт ещё не сформирован.";
        public string report
        {
            get => _report;
            set => Set(ref _report, value);
        }
        #endregion

        #region Timer
        public System.Timers.Timer updatingDateTimeTimer = new System.Timers.Timer()
        {
            Interval = 1000,
            AutoReset = true,
            Enabled = true,
        };
        #endregion

        #region Commands
        //Добавить запись в таблицу
        public ICommand appendRecord { get; }
        private void onAppendCommand(object o)
        {
            DataRecord record = new DataRecord()
            {
                temperature = temperature,
                condition = condition,
                note = note,
                date = date,
                time = time,
            };

            dataSet.Add(record);
        }

        //Сформировать отчёт
        public ICommand formReport { get; }

        private void onFormReport(object o)
        {
        }
        #endregion

        public MainWindowViewModel()
        {
            appendRecord = new ActionCommand(onAppendCommand);

            updatingDateTimeTimer.Elapsed += (object source, ElapsedEventArgs e) =>
            {
                date = DateTime.Now.ToString("d");
                time = DateTime.Now.ToString("HH:mm:ss");
            };
        }
    }
}
