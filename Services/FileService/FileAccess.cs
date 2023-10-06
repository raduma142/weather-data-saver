/* Сервис для работы с файлами программы */

using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.FileService
{
    internal class FileAccess : IFileAccess
    {
        private readonly ILogger<FileAccess> _logger;

        //Сохранение данных в файл в формате JSON
        public string SaveDataSetJSON(ObservableCollection<DataRecord> dataSet, string path)
        {
            CheckPathExists(path);
            string name = string.Format("Отчёт от {0} (c {1} по {2})", DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"), dataSet.First().date, dataSet.Last().date);
            string reportPath = GenerateFileName(path, name, "json");

            _logger.LogInformation($"Сохранён отчёт {reportPath}");

            return reportPath;
        }

        //Сохранение данных в файл в формате JSON
        public string SaveDataSetXML(ObservableCollection<DataRecord> dataSet, string path)
        {
            CheckPathExists(path);
            string name = string.Format("Отчёт от {0} (c {1} по {2})", DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"), dataSet.First().date, dataSet.Last().date);
            string reportPath = GenerateFileName(path, name, "xml");

            _logger.LogInformation($"Сохранён отчёт {reportPath}");

            return reportPath;
        }

        //Сохранение данных в файл в формате CSV
        public string SaveDataSetCSV(ObservableCollection<DataRecord> dataSet, string path)
        {
            CheckPathExists(path);

            string name = string.Format("Отчёт от {0} (c {1} по {2})", DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"), dataSet.First().date, dataSet.Last().date);
            string reportPath = GenerateFileName(path, name, "csv");

            //Сохранение отчёта
            using (var file = new StreamWriter(reportPath, false, Encoding.Default))
            {
                file.WriteLine("date;time;temperature;condition;note");
                foreach (var record in dataSet)
                {
                    string s = record.toCsvString();
                    file.WriteLine(s);
                }
            }

            _logger.LogInformation($"Сохранён отчёт {reportPath}");

            return reportPath;
        }

        //Открыть папку с файлами
        public void OpenFilesFolder(string path)
        {
            CheckPathExists(path);
            Process.Start("explorer.exe", path);
        }

        //Проверить существование папки
        private void CheckPathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        //Сформировать имя файла с учётом существубщих файлов
        private string GenerateFileName(string path, string name, string format)
        {
            string reportFileName = name + "." + format;
            string reportPath = Path.Combine(path, reportFileName);

            //Проверка существования отчёта с таким именем
            while (File.Exists(reportPath))
            {
                name += " (2)";
                reportFileName = name + "." + format;
                reportPath = Path.Combine(path, reportFileName);
            }

            return reportPath;
        }

        public FileAccess(ILogger<FileAccess> logger)
        {
            _logger = logger;
        }
    }
}
