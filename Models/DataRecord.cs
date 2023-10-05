namespace WeatherDataSaver.Models
{
    public class DataRecord
    {
        public float temperature { get; set; } = 0f;
        public string condition { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
        public string time { get; set; } = string.Empty;
        public string toCsvString()
        {
            string csv = string.Format("{0};{1};{2};{3};{4}", date, time, temperature.ToString(), condition, note);
            return csv;
        }
        public string toReportSctring()
        {
            string line = string.Format("\n{0} в {1}\n", date, time);
            line += string.Format("На улице {0}, значение температуры равно {1}℃.\n{2}\n", condition, temperature, note);
            return line;
        }
    }
}
