﻿/* Сервис для сохранения данных в файл */

using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
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

        string docPath, dataPath, csvPath;

        //Сохранение данных в файл
        public string SaveDataSet(ObservableCollection<DataRecord> dataSet)
        {
            string reportName = string.Format("Отчёт от {0} (c {1} по {2})", DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"), dataSet.First().date, dataSet.Last().date);
            string reportFileName = reportName + ".csv";
            string reportPath = Path.Combine(csvPath, reportFileName);

            //Проверка существования отчёта с таким именем
            while (File.Exists(reportPath))
            {
                reportName += " (2)";
                reportFileName = reportName + ".csv";
                reportPath = Path.Combine(csvPath, reportFileName);
            }

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
        public void OpenFilesFolder()
        {
            Process.Start("explorer.exe", csvPath);
        }

        public FileAccess(ILogger<FileAccess> logger)
        {
            docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dataPath = docPath + @"\WeatherData";
            csvPath = dataPath + @"\csv";

            //Проверка существования папки программы
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }

            //Проверка существования папки csv-отчётов
            if (!Directory.Exists(csvPath))
            {
                Directory.CreateDirectory(csvPath);
            }

            _logger = logger;
        }
    }
}
