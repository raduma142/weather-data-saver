using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.FileService
{
    public interface IFileAccess
    {
        string SaveDataSet(ObservableCollection<DataRecord> dataSet);
    }
}