using NewsAgregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class WeatherStatusVM
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public List<WeatherVM>? MorningWeathers { get; set; }
        public List<WeatherVM>? DayWeathers { get; set; }
        public List<WeatherVM>? EveningWeathers { get; set; }
        public List<WeatherVM>? NightWeathers { get; set; }
        public List<WeatherVM>? CommonWeathers { get; set; }
    }
}
