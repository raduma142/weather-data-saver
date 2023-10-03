using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.ReportService
{
    class ReportService
    {
        private readonly ILogger<ReportService> _logger;
        public string CreateReport(ObservableCollection<DataRecord> dataSet)
        {
            return "Report string";
        }
        public ReportService(ILogger<ReportService> logger)
        {
            _logger = logger;
        }
    }
}
