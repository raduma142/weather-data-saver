using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.DataBaseService
{
    public class DataBaseAccess : IDataBaseAccess
    {
        string docPath, dataPath, databasePath, databaseFilePath;
        public DataBaseAccess()
        {
            docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dataPath = docPath + @"\WeatherData";
            databasePath = dataPath + @"\database";
        }
        //Сохранить данные в базу данных
        public string SaveDataSet(ObservableCollection<DataRecord> dataSet, string? databaseName = "database.db")
        {
            databaseFilePath = databasePath + $"\\{databaseName}";
            string connectionString = $"Data Source={databaseFilePath}";

            using (new SqliteConnection(connectionString))
            {
            }

            return databaseFilePath;
        }
    }
}
