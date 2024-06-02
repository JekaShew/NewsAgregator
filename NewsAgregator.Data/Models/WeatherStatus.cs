using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class WeatherStatus
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public List<Weather>? MorningWeathers { get; set; }
        public List<Weather>? DayWeathers { get; set; }
        public List<Weather>? EveningWeathers { get; set; }
        public List<Weather>? NightWeathers { get; set; }
        public List<Weather>? CommonWeathers { get; set; }

    }
}
