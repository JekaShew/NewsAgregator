using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.Data;
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
        private readonly IMapper _mapper;
        public WeatherStatusServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }
        public async Task AddWeatherStatus(WeatherStatusVM weatherStatus)
        {
            var newWeatherStatus = _mapper.Map<Data.Models.WeatherStatus>(weatherStatus);
            newWeatherStatus.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newWeatherStatus);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteWeatherStatus(Guid id)
        {
            _appDBContext.WeatherStatuses.Remove(await _appDBContext.WeatherStatuses.FirstOrDefaultAsync(ws => ws.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<WeatherStatusVM> TakeWeatherStatusById(Guid id)
        {
            var weatherStatus = _mapper.Map<WeatherStatusVM>(await _appDBContext.WeatherStatuses.AsNoTracking().FirstOrDefaultAsync(ws => ws.Id == id));

            return weatherStatus;
        }

        public async Task<List<WeatherStatusVM>> TakeWeatherStatuses()
        {
            var weatherStatusVMs = _mapper.Map<List<WeatherStatusVM>>(await _appDBContext.WeatherStatuses.AsNoTracking().ToListAsync());

            return weatherStatusVMs;
        }

        public async Task UpdateWeatherStatus(WeatherStatusVM updatedWeatherStatus)
        {
            var weatherStatus = await _appDBContext.WeatherStatuses.FirstOrDefaultAsync(ws => ws.Id == updatedWeatherStatus.Id);

            if (weatherStatus != null)
            {
                weatherStatus.Title = updatedWeatherStatus.Title;
                weatherStatus.Description = updatedWeatherStatus.Description;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
