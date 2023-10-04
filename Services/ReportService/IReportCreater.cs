using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.ReportService
{
    public interface IReportCreater
    {
        string CreateReport(ObservableCollection<DataRecord> dataSet);
    }
}