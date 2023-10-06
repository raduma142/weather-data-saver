/* Сервис для работы с файлами программы */

using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.FileService
{
    internal class FileAccess : IFileAccess
    {
        //Сохранение данных в файл в формате JSON
        public string SaveDataSetJSON(ObservableCollection<DataRecord> dataSet, string path)
        {
            CheckPathExists(path);
            string name = string.Format("Отчёт от {0} (c {1} по {2})", DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"), dataSet.First().date, dataSet.Last().date);
            string fileFullPath = GenerateFileName(path, name, "json");

            var options = new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(dataSet, options);
            File.WriteAllText(fileFullPath, jsonString);

            Log.Information("Save a JSON File " + fileFullPath);

            return fileFullPath;
        }

        //Сохранение данных в файл в формате XML
        public string SaveDataSetXML(ObservableCollection<DataRecord> dataSet, string path)
        {
            CheckPathExists(path);
            string name = string.Format("Отчёт от {0} (c {1} по {2})", DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"), dataSet.First().date, dataSet.Last().date);
            string fileFullPath = GenerateFileName(path, name, "xml");

            XmlDocument xmlDocument = new XmlDocument();

            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.AppendChild(xmlDeclaration);

            XmlElement elementDataSet = xmlDocument.CreateElement("meteodata");
            xmlDocument.AppendChild(elementDataSet);

            foreach (var record in dataSet)
            {
                XmlElement elementRecord = xmlDocument.CreateElement("record");

                XmlElement elementDate = xmlDocument.CreateElement("date");
                XmlElement elementTime = xmlDocument.CreateElement("time");
                XmlElement elementTemperature = xmlDocument.CreateElement("temperature");
                XmlElement elementCondition = xmlDocument.CreateElement("condition");
                XmlElement elementNote = xmlDocument.CreateElement("note");
                
                XmlText textDate = xmlDocument.CreateTextNode(record.date);
                XmlText textTime = xmlDocument.CreateTextNode(record.time);
                XmlText textTemperature = xmlDocument.CreateTextNode(record.temperature.ToString());
                XmlText textCondition = xmlDocument.CreateTextNode(record.condition);
                XmlText textNote = xmlDocument.CreateTextNode(record.note);

                elementDate.AppendChild(textDate);
                elementTime.AppendChild(textTime);
                elementTemperature.AppendChild(textTemperature);
                elementCondition.AppendChild(textCondition);
                elementNote.AppendChild(textNote);

                elementRecord.AppendChild(elementDate);
                elementRecord.AppendChild(elementTime);
                elementRecord.AppendChild(elementTemperature);
                elementRecord.AppendChild(elementCondition);
                elementRecord.AppendChild(elementNote);

                elementDataSet.AppendChild(elementRecord);
            }

            xmlDocument.Save(fileFullPath);

            Log.Information("Save a XML File " + fileFullPath);

            return fileFullPath;
        }

        //Сохранение данных в файл в формате CSV
        public string SaveDataSetCSV(ObservableCollection<DataRecord> dataSet, string path)
        {
            CheckPathExists(path);

            string name = string.Format("Отчёт от {0} (c {1} по {2})", DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"), dataSet.First().date, dataSet.Last().date);
            string fileFullPath = GenerateFileName(path, name, "csv");

            //Сохранение отчёта
            using (var file = new StreamWriter(fileFullPath, false, Encoding.Default))
            {
                file.WriteLine("date;time;temperature;condition;note");
                foreach (var record in dataSet)
                {
                    string s = record.toCsvString();
                    file.WriteLine(s);
                }
            }

            Log.Information("Save a CSV File " + fileFullPath);

            return fileFullPath;
        }

        //Открыть папку с файлами
        public void OpenFilesFolder(string path)
        {
            CheckPathExists(path);

            Log.Information("Open Path In Explorer " + path);

            Process.Start("explorer.exe", path);
        }

        //Проверить существование папки
        private void CheckPathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Log.Information("Create Files Directory" + path);

                Directory.CreateDirectory(path);
            }
        }

        //Сформировать имя файла с учётом существубщих файлов
        private string GenerateFileName(string path, string name, string format)
        {
            string reportFileName = name + "." + format;
            string fileFullPath = Path.Combine(path, reportFileName);

            //Проверка существования отчёта с таким именем
            while (File.Exists(fileFullPath))
            {
                name += " (2)";
                reportFileName = name + "." + format;
                fileFullPath = Path.Combine(path, reportFileName);
            }

            Log.Information("Create File Name " + reportFileName);

            return fileFullPath;
        }
    }
}
