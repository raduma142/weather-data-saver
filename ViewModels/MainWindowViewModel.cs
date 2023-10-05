using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Timers;
using System.Windows.Input;
using WeatherDataSaver.Infrascructure.Commands;
using WeatherDataSaver.Models;
using WeatherDataSaver.Services.DataBaseService;
using WeatherDataSaver.Services.FileService;
using WeatherDataSaver.Services.ReportService;
using WeatherDataSaver.ViewModels.Base;

namespace WeatherDataSaver.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Services
        public IDataBaseAccess dataBaseAccess;
        public IReportCreater reportCreater;
        public IFileAccess fileAccess;
        #endregion

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

        //Condition Variants
        public ObservableCollection<string[]> conditions { get; } = new ObservableCollection<string[]>()
        {
            new string[]{"☀", "Ясно"},
            new string[]{"☁", "Облачно"},
            new string[]{"🌫", "Туман"},
            new string[]{"💧", "Дождь"},
            new string[]{"❄", "Снег"},
            new string[]{"🧊", "Град"},
        };

        //Selected Condition Index
        private int _condition_index = 0;
        public int condition_index
        {
            
            get => _condition_index;
            set
            {
                condition = conditions[value][1];
                Set(ref _condition_index, value);
            }
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

        //Saved File Path
        private string _csvPath = "Записи ещё не сохранены в файл.";
        public string csvPath
        {
            get => _csvPath;
            set => Set(ref _csvPath, value);
        }

        //Saved Database Path
        private string _databasePath = "Записи ещё не сохранены в базу данных.";
        public string databasePath
        {
            get => _databasePath;
            set => Set(ref _databasePath, value);
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
        public ICommand createReport { get; }

        private void onCreateReport(object o)
        {
            report = reportCreater.CreateReport(dataSet);
        }

        //Сохранить отчёт в файл
        public ICommand saveReportToFile { get; }
        private bool canWorkWithReport(object o)
        {
            return dataSet.Count > 0;
        }
        private void onSaveReportToFile(object o)
        {
            csvPath = fileAccess.SaveDataSet(dataSet);
        }

        //Сохранить отчёт в базу данных
        public ICommand saveReportToDataBase { get; }
        private void onSaveReportToDataBase(object o)
        {
            databasePath = dataBaseAccess.SaveDataSet(dataSet, ConfigurationManager.AppSettings.Get("DataBaseName"));
        }

        //Открыть папку с файлами
        public ICommand openFilesFolder { get; }
        private void onOpenFilesFolder(object o)
        {
            fileAccess.OpenFilesFolder((string) o);
        }
        #endregion

        public MainWindowViewModel()
        {
            appendRecord =          new ActionCommand(onAppendCommand);
            createReport =          new ActionCommand(onCreateReport, canWorkWithReport);
            saveReportToFile =      new ActionCommand(onSaveReportToFile, canWorkWithReport);
            saveReportToDataBase =  new ActionCommand(onSaveReportToDataBase, canWorkWithReport);
            openFilesFolder =       new ActionCommand(onOpenFilesFolder);

            //Автоматическое обновление времени
            updatingDateTimeTimer.Elapsed += (object source, ElapsedEventArgs e) =>
            {
                date = DateTime.Now.ToString("d");
                time = DateTime.Now.ToString("HH:mm:ss");
            };
        }
    }
}
