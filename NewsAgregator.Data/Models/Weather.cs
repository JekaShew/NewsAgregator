using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class Weather
    {
        public Guid Id { get; set; }
        public string? City { get; set; }
        public int? TemperatureMorning { get; set; }
        public int? TemperatureDay { get; set; }
        public int? TemperatureEvening { get; set; }
        public int? TemperatureNight { get; set; }
        public int? TemperatureCommon { get; set; }
        public DateTime? Date { get; set; }
        public string? Percipitaion { get; set; }
        public float? Wind { get; set; }
        public string? WindDirection { get; set; }
        public int? Pressure { get; set; }
        public int? Humidity { get; set; }

        public Guid? WeatherStatusMorningId { get; set; }
        public WeatherStatus? WeatherStatusMorning { get; set; }

        public Guid? WeatherStatusDayId { get; set; }
        public WeatherStatus? WeatherStatusDay { get; set; }

        public Guid? WeatherStatusEveningId { get; set; }
        public WeatherStatus? WeatherStatusEvening { get; set; }

        public Guid? WeatherStatusNightId { get; set; }
        public WeatherStatus? WeatherStatusNight { get; set; }

        public Guid? WeatherStatusCommonId { get; set; }
        public WeatherStatus? WeatherStatusCommon { get; set; }

    }
}
