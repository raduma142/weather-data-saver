/* Сервис генерации отчётов */

using Serilog;
using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.ReportService
{
    class ReportCreater : IReportCreater
    {
        //Создать отчёт
        public string CreateReport(ObservableCollection<DataRecord> dataSet)
        {
            string report = "Отчёт\n";

            foreach(var record in dataSet)
            {
                report += record.toReportSctring();
            }

            Log.Information("Create a Text Report");

            return report;
        }
    }
}
