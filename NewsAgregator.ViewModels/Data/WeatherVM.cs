using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class WeatherVM
    {
        [Required]
        public Guid Id { get; set; }
        [Required, MinLength(2)]
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


        public List<Parameter>? WeatherStatuses { get; set; }
        public Guid? WeatherStatusMorningId { get; set; }
        public Parameter? WeatherStatusMorning { get; set; }

        public Guid? WeatherStatusDayId { get; set; }
        public Parameter? WeatherStatusDay { get; set; }

        public Guid? WeatherStatusEveningId { get; set; }
        public Parameter? WeatherStatusEvening { get; set; }

        public Guid? WeatherStatusNightId { get; set; }
        public Parameter? WeatherStatusNight { get; set; }

        public Guid? WeatherStatusCommonId { get; set; }
        public Parameter? WeatherStatusCommon { get; set; }

        public void FromDataModel(WeatherStatus weatherStatusCommon, WeatherStatus weatherStatusMorning, WeatherStatus weatherStatusDay, WeatherStatus weatherStatusEvening, WeatherStatus weatherStatusNight)
        {        
            WeatherStatusCommon = new Parameter
            {
                Id = weatherStatusCommon !=null? weatherStatusCommon.Id : null,
                Text = weatherStatusCommon != null ? weatherStatusCommon.Title : "",
            };

            WeatherStatusMorning = new Parameter
            {
                Id = weatherStatusMorning != null ? weatherStatusMorning.Id : null,
                Text = weatherStatusMorning != null ? weatherStatusMorning.Title : "",
            };

            WeatherStatusDay = new Parameter
            {
                Id = weatherStatusDay != null ? weatherStatusDay.Id : null,
                Text = weatherStatusDay != null ? weatherStatusDay.Title : "",
            };

            WeatherStatusEvening = new Parameter
            {
                Id = weatherStatusEvening != null ? weatherStatusEvening.Id : null,
                Text = weatherStatusEvening != null ? weatherStatusEvening.Title : "",
            };

            WeatherStatusNight = new Parameter
            {
                Id = weatherStatusNight != null ? weatherStatusNight.Id : null,
                Text = weatherStatusNight != null ? weatherStatusNight.Title : "",
            };

        }
    }
}
