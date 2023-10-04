using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.ReportService
{
    class ReportCreater : IReportCreater
    {
        private readonly ILogger<ReportCreater> _logger;
        public string CreateReport(ObservableCollection<DataRecord> dataSet)
        {
            string report = "Отчёт\n\n";

            foreach(var record in dataSet)
            {
                report += $"{record.date} в {record.time}\n";
                report += $"На улице {record.condition}, значние температуры равно {record.temperature}℃.\n{record.note}\n\n";
            }

            return report;
        }
        public ReportCreater(ILogger<ReportCreater> logger)
        {
            _logger = logger;
        }
    }
}
