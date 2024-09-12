using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.WeatherInterfaces
{
    public interface IWeatherStatusServices
    {
        public Task<List<WeatherStatusVM>> TakeWeatherStatusesAsync();

        public Task<WeatherStatusVM> TakeWeatherStatusByIdAsync(Guid id);

        public Task AddWeatherStatusAsync(WeatherStatusVM weatherStatus);

        public Task DeleteWeatherStatusAsync(Guid id);

        public Task UpdateWeatherStatusAsync(WeatherStatusVM updatedWeatherStatus);
    }
}
