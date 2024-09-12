using NewsAgregator.ViewModels.Additional;
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
        public Task<List<WeatherVM>> TakeWeathersAsync();

        public Task<WeatherVM> TakeWeatherByIdAsync(Guid id);

        public Task AddWeatherAsync(WeatherVM weather);

        public Task DeleteWeatherAsync(Guid id);

        public Task UpdateWeatherAsync(WeatherVM updatedWeather);
        public Task<WeatherParameters> GetWeatherParametersAsync();
    }
}
