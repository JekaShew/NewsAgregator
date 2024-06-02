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
        public Task<List<WeatherStatusVM>> TakeWeatherStatuses();

        public Task<WeatherStatusVM> TakeWeatherStatusById(Guid id);

        public Task AddWeatherStatus(WeatherStatusVM weatherStatus);

        public Task DeleteWeatherStatus(Guid id);

        public Task UpdateWeatherStatus(WeatherStatusVM updatedWeatherStatus);
    }
}
