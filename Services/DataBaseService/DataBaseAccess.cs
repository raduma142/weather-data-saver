﻿using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.DataBaseService
{
    public class DataBaseAccess : IDataBaseAccess
    {
        ILogger<DataBaseAccess> _logger;
        public DataBaseAccess(ILogger<DataBaseAccess> logger)
        {
            _logger = logger;
        }
        //Сохранить данные в базу данных
        public string SaveDataSet(ObservableCollection<DataRecord> dataSet, string path, string databaseName)
        {
            CheckPathExists(path);
            string databaseFilePath = path + $"\\{databaseName}";
            string connectionString = $"Data Source={databaseFilePath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                _logger.LogInformation("Подключено к базе данных");

                //Создание таблицы (при отсутствии)
                SqliteCommand commandCreate = connection.CreateCommand();
                commandCreate.Connection = connection;
                commandCreate.CommandText = "CREATE TABLE IF NOT EXISTS meteodata(date TEXT NOT NULL, time TEXT NOT NULL, temperature INTEGER NOT NULL, condition TEXT NOT NULL, note TEXT)";
                commandCreate.ExecuteNonQuery();

                _logger.LogInformation("Выполнен запрос создание таблицы (при отсутствии)");

                //Добавление записей
                SqliteCommand commandInsert = connection.CreateCommand();
                commandInsert.Connection = connection;
                foreach (var record in dataSet)
                {
                    commandInsert.CommandText = $"INSERT INTO meteodata(date, time, temperature, condition, note) VALUES ('{record.date}', '{record.time}', {record.temperature.ToString(CultureInfo.InvariantCulture)}, '{record.condition}', '{record.note}')";
                    commandInsert.ExecuteNonQuery();
                }
                connection.Close();
            }

            return databaseFilePath;
        }

        //Проверить существование папки
        private void CheckPathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
