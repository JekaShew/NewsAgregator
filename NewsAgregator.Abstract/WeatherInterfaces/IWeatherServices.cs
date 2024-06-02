using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.WeatherInterfaces
{
    public interface IWeatherServices
    {
        public Task<List<WeatherVM>> TakeWeathers();

        public Task<WeatherVM> TakeWeatherById(Guid id);

        public Task AddWeather(WeatherVM weather);

        public Task DeleteWeather(Guid id);

        public Task UpdateWeather(WeatherVM updatedWeather);
    }
}
