using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Mapper.DataMappers
{
    [Mapper]
    public static partial class WeatherMapper
    {
        [MapProperty([nameof(Weather.WeatherStatusCommon), nameof(Weather.WeatherStatusCommon.Id)],
            [nameof(WeatherVM.WeatherStatusCommon), nameof(WeatherVM.WeatherStatusCommon.Id)])]
        [MapProperty([nameof(Weather.WeatherStatusCommon), nameof(Weather.WeatherStatusCommon.Title)],
            [nameof(WeatherVM.WeatherStatusCommon), nameof(WeatherVM.WeatherStatusCommon.Text)])]

        [MapProperty([nameof(Weather.WeatherStatusMorning), nameof(Weather.WeatherStatusMorning.Id)],
            [nameof(WeatherVM.WeatherStatusMorning), nameof(WeatherVM.WeatherStatusMorning.Id)])]
        [MapProperty([nameof(Weather.WeatherStatusMorning), nameof(Weather.WeatherStatusMorning.Title)],
            [nameof(WeatherVM.WeatherStatusMorning), nameof(WeatherVM.WeatherStatusMorning.Text)])]

        [MapProperty([nameof(Weather.WeatherStatusDay), nameof(Weather.WeatherStatusDay.Id)],
            [nameof(WeatherVM.WeatherStatusDay), nameof(WeatherVM.WeatherStatusDay.Id)])]
        [MapProperty([nameof(Weather.WeatherStatusDay), nameof(Weather.WeatherStatusDay.Title)],
            [nameof(WeatherVM.WeatherStatusDay), nameof(WeatherVM.WeatherStatusDay.Text)])]

        [MapProperty([nameof(Weather.WeatherStatusEvening), nameof(Weather.WeatherStatusEvening.Id)],
            [nameof(WeatherVM.WeatherStatusEvening), nameof(WeatherVM.WeatherStatusEvening.Id)])]
        [MapProperty([nameof(Weather.WeatherStatusEvening), nameof(Weather.WeatherStatusEvening.Title)],
            [nameof(WeatherVM.WeatherStatusEvening), nameof(WeatherVM.WeatherStatusEvening.Text)])]

        [MapProperty([nameof(Weather.WeatherStatusNight), nameof(Weather.WeatherStatusNight.Id)],
            [nameof(WeatherVM.WeatherStatusNight), nameof(WeatherVM.WeatherStatusNight.Id)])]
        [MapProperty([nameof(Weather.WeatherStatusNight), nameof(Weather.WeatherStatusNight.Title)],
            [nameof(WeatherVM.WeatherStatusNight), nameof(WeatherVM.WeatherStatusNight.Text)])]
        public static partial WeatherVM? WeatherToWeatherVM(Weather? weather);

        public static partial Weather? WeatherVMToWeather(WeatherVM? weatherVM);

    }
}
