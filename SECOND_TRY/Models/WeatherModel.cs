namespace MercuryTech_Test.Models
{
    public class WeatherModel
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public DateTime? RecordTime { get; set; }
    }
}