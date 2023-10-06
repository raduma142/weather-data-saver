/* Сервис для работы с файлами программы */

using System.Collections.ObjectModel;
using WeatherDataSaver.Models;

namespace WeatherDataSaver.Services.FileService
{
    public interface IFileAccess
    {
        void OpenFilesFolder(string path);
        string SaveDataSetCSV(ObservableCollection<DataRecord> dataSet, string path);
        string SaveDataSetJSON(ObservableCollection<DataRecord> dataSet, string path);
        string SaveDataSetXML(ObservableCollection<DataRecord> dataSet, string path);
    }
}