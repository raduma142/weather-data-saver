using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.ReportService
{
    class ReportCreater : IReportCreater
    {
        private readonly ILogger<ReportCreater> _logger;
        public string CreateReport(ObservableCollection<DataRecord> dataSet)
        {
            return "Report \n string";
        }
        public ReportCreater(ILogger<ReportCreater> logger)
        {
            _logger = logger;
        }
    }
}
