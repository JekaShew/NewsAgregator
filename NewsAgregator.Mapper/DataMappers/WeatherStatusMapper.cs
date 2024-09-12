using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Mappers
{
    [Mapper]
    public static partial class WeatherStatusMapper
    {
        public static partial WeatherStatusVM? WeatherStatusToWeatherStatusVM(WeatherStatus? weatherStatus);

        public static partial WeatherStatus? WeatherStatusVMToWeatherStatus(WeatherStatusVM? weatherStatusVM);

    }
}
