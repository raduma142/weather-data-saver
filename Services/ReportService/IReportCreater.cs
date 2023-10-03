using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.ReportService
{
    internal interface IReportCreater
    {
        string CreateReport(ObservableCollection<DataRecord> dataSet);
    }
}