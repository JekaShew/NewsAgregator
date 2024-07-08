using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.WeatherServices
{
    public class WeatherServices : IWeatherServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;
        public WeatherServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }

        public async Task<WeatherParameters> GetWeatherParameters()
        {
            var weatherParameters = new WeatherParameters()
            {
                WeatherStatuses = await _appDBContext.WeatherStatuses.Select(accs => new Parameter { Id = accs.Id, Text = accs.Title }).ToListAsync(),
            };
            return weatherParameters;

        }

        public async Task ConvertWeatherParameters(WeatherVM weatherVM)
        {
            var weatherStatusCommon = await _appDBContext.WeatherStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == weatherVM.WeatherStatusCommonId);
            var weatherStatusMorning = await _appDBContext.WeatherStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == weatherVM.WeatherStatusMorningId);
            var weatherStatusDay = await _appDBContext.WeatherStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == weatherVM.WeatherStatusDayId);
            var weatherStatusEvening = await _appDBContext.WeatherStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == weatherVM.WeatherStatusEveningId);
            var weatherStatusNight = await _appDBContext.WeatherStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == weatherVM.WeatherStatusNightId);

            weatherVM.FromDataModel(weatherStatusCommon, weatherStatusMorning, weatherStatusDay, weatherStatusEvening, weatherStatusNight);
        }

        public async Task AddWeather(WeatherVM weather)
        {
            var newWeather = _mapper.Map<Data.Models.Weather>(weather);
            newWeather.Id = Guid.NewGuid();
            newWeather.WeatherStatusMorning = null;
            newWeather.WeatherStatusDay = null;
            newWeather.WeatherStatusEvening = null;
            newWeather.WeatherStatusNight = null;
            newWeather.WeatherStatusCommon = null;

            await _appDBContext.AddAsync(newWeather);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteWeather(Guid id)
        {
            _appDBContext.Weathers.Remove(await _appDBContext.Weathers.FirstOrDefaultAsync(w => w.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<WeatherVM> TakeWeatherById(Guid id)
        {
            var weather = _mapper.Map<WeatherVM>(await _appDBContext.Weathers
                .AsNoTracking()
                .Include(wsd => wsd.WeatherStatusDay)
                .Include(wsm => wsm.WeatherStatusMorning)
                .Include(wse => wse.WeatherStatusEvening)
                .Include(wsn => wsn.WeatherStatusNight)
                .Include(wsc => wsc.WeatherStatusCommon)
                .FirstOrDefaultAsync(w => w.Id == id));

            var weatherParameters = await GetWeatherParameters();
            await ConvertWeatherParameters(weather);
            weather.WeatherStatuses = weatherParameters.WeatherStatuses;

            return weather;
        }

        public async Task<List<WeatherVM>> TakeWeathers()
        {
            var weatherVMs = _mapper.Map<List<WeatherVM>>(await _appDBContext.Weathers
                .AsNoTracking()
                .Include(wsd => wsd.WeatherStatusDay)
                .Include(wsm => wsm.WeatherStatusMorning)
                .Include(wse => wse.WeatherStatusEvening)
                .Include(wsn => wsn.WeatherStatusNight)
                .Include(wsc => wsc.WeatherStatusCommon)
                .ToListAsync());

            foreach (var weatherVM in weatherVMs)
            {
                await ConvertWeatherParameters(weatherVM);  
            }

            return weatherVMs;
        }

        public async Task UpdateWeather(WeatherVM updatedWeather)
        {
            var weather = await _appDBContext.Weathers.FirstOrDefaultAsync(w => w.Id == updatedWeather.Id);

            if (weather != null)
            {
                weather.City = updatedWeather.City;
                weather.TemperatureMorning = updatedWeather.TemperatureMorning;
                weather.TemperatureDay = updatedWeather.TemperatureDay;
                weather.TemperatureEvening = updatedWeather.TemperatureEvening;
                weather.TemperatureNight = updatedWeather.TemperatureNight;
                weather.TemperatureCommon = updatedWeather.TemperatureCommon;
                weather.Date = updatedWeather.Date;
                weather.Percipitaion = updatedWeather.Percipitaion;
                weather.Wind = updatedWeather.Wind;
                weather.WindDirection = updatedWeather.WindDirection;
                weather.Pressure = updatedWeather.Pressure;
                weather.Humidity = updatedWeather.Humidity;
                weather.WeatherStatusMorningId = updatedWeather.WeatherStatusMorningId;
                weather.WeatherStatusMorning = null;
                weather.WeatherStatusDayId = updatedWeather.WeatherStatusDayId;
                weather.WeatherStatusDay = null;
                weather.WeatherStatusEveningId = updatedWeather.WeatherStatusEveningId;
                weather.WeatherStatusEvening = null;
                weather.WeatherStatusNightId = updatedWeather.WeatherStatusNightId;
                weather.WeatherStatusNight = null;
                weather.WeatherStatusCommonId = updatedWeather.WeatherStatusCommonId;
                weather.WeatherStatusCommon = null;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
