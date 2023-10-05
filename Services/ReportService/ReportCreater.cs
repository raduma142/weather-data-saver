/* Сервис генерации отчётов */

using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.ReportService
{
    class ReportCreater : IReportCreater
    {
        private readonly ILogger<ReportCreater> _logger;

        //Создать отчёт
        public string CreateReport(ObservableCollection<DataRecord> dataSet)
        {
            string report = "Отчёт\n";

            foreach(var record in dataSet)
            {
                report += record.toReportSctring();
            }

            _logger.LogInformation("Сгенерирован отчёт");

            return report;
        }
        public ReportCreater(ILogger<ReportCreater> logger)
        {
            _logger = logger;
        }
    }
}
