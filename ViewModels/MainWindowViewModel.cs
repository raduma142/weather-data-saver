using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Timers;
using System.Windows;
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
        public ObservableCollection<DataRecord> dataSet { get; set; } = new ObservableCollection<DataRecord>();

        //Selected DataSet Recorf
        private DataRecord _selectedRecord;
        public DataRecord selectedRecord
        {
            get => _selectedRecord;
            set => Set(ref _selectedRecord, value);
        }

        //Temperature
        private float _temperature = 0f;
        public float temperature
        {
            get => _temperature;
            set
            {
                if (value > 100) value = 100;
                if (value < -100) value = 100;
                Set(ref _temperature, value);
            }
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
        private string _filePath = "Записи ещё не сохранены в файл.";
        public string filePath
        {
            get => _filePath;
            set => Set(ref _filePath, value);
        }

        //Save Files Directory Path
        string saveFilesPath;

        //Format Save File (JSON, XML, CSV)
        string formatSaveFile;

        //Save Database Directiory Path
        string saveDatabasePath;

        //Database File Name
        string dataBaseFileName;

        //Saved Database Path
        private string _databasePath = "Записи ещё не сохранены в базу данных.";
        public string databasePath
        {
            get => _databasePath;
            set => Set(ref _databasePath, value);
        }

        //SaveToFile Visibility
        private Visibility _saveToFileVisibility = Visibility.Visible;
        public Visibility saveToFileVisibility
        {
            get => _saveToFileVisibility;
            set => Set(ref _saveToFileVisibility, value);
        }

        //SaveToDatabase Visibility
        private Visibility _saveToDatabaseVisibility = Visibility.Visible;
        public Visibility saveToDatabaseVisibility
        {
            get => _saveToDatabaseVisibility;
            set => Set(ref _saveToDatabaseVisibility, value);
        }

        //CreateReport Visibility
        private Visibility _createReportVisibility = Visibility.Visible;
        public Visibility createReportVisibility
        {
            get => _createReportVisibility;
            set => Set(ref _createReportVisibility, value);
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
            if (ConfigurationManager.AppSettings.Get("CanCreateReport") == "yes")
            {
                report = reportCreater.CreateReport(dataSet);
            }
        }

        //Сохранить отчёт в файл
        public ICommand saveReportToFile { get; }
        private bool canWorkWithReport(object o)
        {
            return dataSet.Count > 0;
        }
        private void onSaveReportToFile(object o)
        {
            if (ConfigurationManager.AppSettings.Get("CanSaveToFile") == "yes")
            {
                if (formatSaveFile == "json")
                {
                    filePath = fileAccess.SaveDataSetJSON(dataSet, saveFilesPath);
                }
                else
                if (formatSaveFile == "xml")
                {
                    filePath = fileAccess.SaveDataSetXML(dataSet, saveFilesPath);
                }
                else
                {
                    filePath = fileAccess.SaveDataSetCSV(dataSet, saveFilesPath);
                }
            }
        }

        //Сохранить отчёт в базу данных
        public ICommand saveReportToDataBase { get; }
        private void onSaveReportToDataBase(object o)
        {
            if (ConfigurationManager.AppSettings.Get("CanSaveToDataBase") == "yes")
            {
                databasePath = dataBaseAccess.SaveDataSet(dataSet, saveDatabasePath, dataBaseFileName);
            }
        }

        //Открыть папку с файлами
        public ICommand openFilesFolder { get; }
        private void onOpenFilesFolder(object o)
        {
            if (((string) o) == "files")
                fileAccess.OpenFilesFolder(saveFilesPath);
            else
                fileAccess.OpenFilesFolder(saveDatabasePath);
        }

        //Удалить выделенную запись
        public ICommand deleteSelectedRecord { get; }
        private bool canDeleteSelectedRecord(object o)
        {
            return !(selectedRecord == null);
        }
        private void onDeleteSelectedRecord(object o)
        {
            dataSet.Remove(selectedRecord);
        }

        //Удалить все записи
        public ICommand deleteAllRecords { get; }
        private void onDeleteAllRecords(object o)
        {
            dataSet.Clear();
        }
        #endregion

        public MainWindowViewModel()
        {
            appendRecord =          new ActionCommand(onAppendCommand);
            createReport =          new ActionCommand(onCreateReport, canWorkWithReport);
            saveReportToFile =      new ActionCommand(onSaveReportToFile, canWorkWithReport);
            saveReportToDataBase =  new ActionCommand(onSaveReportToDataBase, canWorkWithReport);
            deleteSelectedRecord =  new ActionCommand(onDeleteSelectedRecord, canDeleteSelectedRecord);
            deleteAllRecords =      new ActionCommand(onDeleteAllRecords);
            openFilesFolder =       new ActionCommand(onOpenFilesFolder);

            //Применение файла конфигурации
            if (ConfigurationManager.AppSettings.Get("CanSaveToFile") == "no")
            {
                saveToFileVisibility = Visibility.Collapsed;
            }
            if (ConfigurationManager.AppSettings.Get("CanCreateReport") == "no")
            {
                createReportVisibility = Visibility.Collapsed;
            }
            if (ConfigurationManager.AppSettings.Get("CanSaveToDataBase") == "no")
            {
                saveToDatabaseVisibility = Visibility.Collapsed;
            }
            string? path = ConfigurationManager.AppSettings.Get("SaveFilesPath");
            if ((path == null) || (path == ""))
            {
                saveFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFilesPath += @"\WeatherData\files";
            }
            else
            {
                saveFilesPath = path;
            }
            path = ConfigurationManager.AppSettings.Get("SaveDatabasePath");
            if ((path == null) || (path == ""))
            {
                saveDatabasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveDatabasePath += @"\WeatherData\database";
            }
            else
            {
                saveDatabasePath = path;
            }
            string? name = ConfigurationManager.AppSettings.Get("DataBaseFileName");
            if (name == null)
            {
                dataBaseFileName = "database.db";
            }
            else
            {
                dataBaseFileName = name;
            }

            //Автоматическое обновление времени
            updatingDateTimeTimer.Elapsed += (object source, ElapsedEventArgs e) =>
            {
                date = DateTime.Now.ToString("d");
                time = DateTime.Now.ToString("HH:mm:ss");
            };
        }
    }
}
