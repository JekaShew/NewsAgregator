using NewsAgregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class WeatherVM
    {
        public Guid Id { get; set; }
        public string? City { get; set; }
        public int? TemperatureMorning { get; set; }
        public int? TemperatureDay { get; set; }
        public int? TemperatureEvening { get; set; }
        public int? TemperatureNight { get; set; }
        public int? TemperatureCommon { get; set; }
        public DateTime? Date { get; set; }
        public string? Percipittaion { get; set; }
        public float? Wind { get; set; }
        public string? WindDirection { get; set; }
        public int? Pressure { get; set; }
        public int? Humidity { get; set; }

        public Guid? WeatherStatusMorningId { get; set; }
        public WeatherStatusVM? WeatherStatusMorning { get; set; }

        public Guid? WeatherStatusDayId { get; set; }
        public WeatherStatusVM? WeatherStatusDay { get; set; }

        public Guid? WeatherStatusEveningId { get; set; }
        public WeatherStatusVM? WeatherStatusEvening { get; set; }

        public Guid? WeatherStatusNightId { get; set; }
        public WeatherStatusVM? WeatherStatusNight { get; set; }

        public Guid? WeatherStatusCommonId { get; set; }
        public WeatherStatusVM? WeatherStatusCommon { get; set; }
    }
}
