using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Mappers.PropertiesMappers
{
    [Mapper]
    public static partial class WeatherParametersMapper
    {
        [MapProperty(nameof(WeatherStatus.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(WeatherStatus.Title), nameof(Parameter.Text))]
        public static partial Parameter? WeatherStatusToParameter(WeatherStatus? weatherStatus);
    }
}