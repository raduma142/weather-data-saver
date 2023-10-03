namespace WeatherDataSaver.Models
{
    public class DataRecord
    {
        public float temperature { get; set; } = 0f;
        public string condition { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
        public string time { get; set; } = string.Empty;
    }
}
