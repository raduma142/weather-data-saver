using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataSaver.Services.DataBaseService
{
    public class DataBaseAccess
    {
        string databasePath = "";
        public DataBaseAccess()
        {
            using (new SqliteConnection(databasePath))
            {

            }
        }
    }
}
