using AutoMapper;
using Mapper.Mappers.PropertiesMappers;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.Mapper.DataMappers;
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
        public WeatherServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<WeatherParameters> GetWeatherParameters()
        {
            var weatherParameters = new WeatherParameters()
            {
                WeatherStatuses = (await _appDBContext.WeatherStatuses.ToListAsync()).Select(ws => WeatherParametersMapper.WeatherStatusToParameter(ws)).ToList(),
            };
            return weatherParameters;

        }

        public async Task AddWeatherAsync(WeatherVM weatherVM)
        {
            var newWeather = WeatherMapper.WeatherVMToWeather(weatherVM);
            newWeather.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newWeather);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteWeatherAsync(Guid id)
        {
            _appDBContext.Weathers.Remove(await _appDBContext.Weathers.FirstOrDefaultAsync(w => w.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<WeatherVM> TakeWeatherByIdAsync(Guid id)
        {
            var weather = WeatherMapper.WeatherToWeatherVM(await _appDBContext.Weathers
                .AsNoTracking()
                .Include(wsd => wsd.WeatherStatusDay)
                .Include(wsm => wsm.WeatherStatusMorning)
                .Include(wse => wse.WeatherStatusEvening)
                .Include(wsn => wsn.WeatherStatusNight)
                .Include(wsc => wsc.WeatherStatusCommon)
                .FirstOrDefaultAsync(w => w.Id == id));

            var weatherParameters = await GetWeatherParameters();
            weather.WeatherStatuses = weatherParameters.WeatherStatuses;

            return weather;
        }

        public async Task<List<WeatherVM>> TakeWeathersAsync()
        {
            var weatherVMs = (await _appDBContext.Weathers
                .AsNoTracking()
                .Include(wsd => wsd.WeatherStatusDay)
                .Include(wsm => wsm.WeatherStatusMorning)
                .Include(wse => wse.WeatherStatusEvening)
                .Include(wsn => wsn.WeatherStatusNight)
                .Include(wsc => wsc.WeatherStatusCommon)
                .ToListAsync()).Select(w => WeatherMapper.WeatherToWeatherVM(w)).ToList();

            return weatherVMs;
        }

        public async Task UpdateWeatherAsync(WeatherVM updatedWeatherVM)
        {
            var weather = await _appDBContext.Weathers.FirstOrDefaultAsync(w => w.Id == updatedWeatherVM.Id);

            if (weather != null)
            {
                weather.City = updatedWeatherVM.City;
                weather.TemperatureMorning = updatedWeatherVM.TemperatureMorning;
                weather.TemperatureDay = updatedWeatherVM.TemperatureDay;
                weather.TemperatureEvening = updatedWeatherVM.TemperatureEvening;
                weather.TemperatureNight = updatedWeatherVM.TemperatureNight;
                weather.TemperatureCommon = updatedWeatherVM.TemperatureCommon;
                weather.Date = updatedWeatherVM.Date;
                weather.Percipitaion = updatedWeatherVM.Percipitaion;
                weather.Wind = updatedWeatherVM.Wind;
                weather.WindDirection = updatedWeatherVM.WindDirection;
                weather.Pressure = updatedWeatherVM.Pressure;
                weather.Humidity = updatedWeatherVM.Humidity;
                weather.WeatherStatusMorningId = updatedWeatherVM.WeatherStatusMorningId;
                weather.WeatherStatusDayId = updatedWeatherVM.WeatherStatusDayId;
                weather.WeatherStatusEveningId = updatedWeatherVM.WeatherStatusEveningId;
                weather.WeatherStatusNightId = updatedWeatherVM.WeatherStatusNightId;
                weather.WeatherStatusCommonId = updatedWeatherVM.WeatherStatusCommonId;

                await _appDBContext.SaveChangesAsync();
            }
        }
    }
}
