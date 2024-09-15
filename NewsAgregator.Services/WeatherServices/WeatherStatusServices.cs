using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data;
using NewsAgregator.Data.Mappers;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.WeatherServices
{
    public class WeatherStatusServices : IWeatherStatusServices
    {
        private readonly AppDBContext _appDBContext;
        public WeatherStatusServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task AddWeatherStatusAsync(WeatherStatusVM weatherStatus)
        {
            var newWeatherStatus = WeatherStatusMapper.WeatherStatusVMToWeatherStatus(weatherStatus);
            newWeatherStatus.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newWeatherStatus);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteWeatherStatusAsync(Guid id)
        {
            _appDBContext.WeatherStatuses.Remove(await _appDBContext.WeatherStatuses.FirstOrDefaultAsync(ws => ws.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<WeatherStatusVM> TakeWeatherStatusByIdAsync(Guid id)
        {
            var weatherStatusVM = WeatherStatusMapper.WeatherStatusToWeatherStatusVM(await _appDBContext.WeatherStatuses.AsNoTracking().FirstOrDefaultAsync(ws => ws.Id == id));

            return weatherStatusVM;
        }

        public async Task<List<WeatherStatusVM>> TakeWeatherStatusesAsync()
        {
            var weatherStatusVMs = (await _appDBContext.WeatherStatuses.AsNoTracking().ToListAsync()).Select(ws => WeatherStatusMapper.WeatherStatusToWeatherStatusVM(ws)).ToList();

            return weatherStatusVMs;
        }

        public async Task UpdateWeatherStatusAsync(WeatherStatusVM updatedWeatherStatus)
        {
            var weatherStatus = await _appDBContext.WeatherStatuses.FirstOrDefaultAsync(ws => ws.Id == updatedWeatherStatus.Id);
            //weatherStatus = WeatherStatusMapper.WeatherStatusVMToWeatherStatus(updatedWeatherStatus);

            if (weatherStatus != null)
            {
                weatherStatus.Title = updatedWeatherStatus.Title;
                weatherStatus.Description = updatedWeatherStatus.Description;

                await _appDBContext.SaveChangesAsync();
            }

        }
    }
}
