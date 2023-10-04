/* Сервис для сохранения данных в файл */

using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.FileService
{
    public interface IFileAccess
    {
        void OpenFilesFolder();
        string SaveDataSet(ObservableCollection<DataRecord> dataSet);
    }
}