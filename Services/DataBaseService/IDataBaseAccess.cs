using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.DataBaseService
{
    public interface IDataBaseAccess
    {
        string SaveDataSet(ObservableCollection<DataRecord> dataSet);
    }
}