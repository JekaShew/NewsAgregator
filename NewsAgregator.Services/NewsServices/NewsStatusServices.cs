using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.NewsServices
{
    public class NewsStatusServices : INewsStatusServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;
        public NewsStatusServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }
        public async Task AddNewsStatus(NewsStatusVM newsStatus)
        {
            var newNewsStatus = _mapper.Map<Data.Models.NewsStatus>(newsStatus);
            newNewsStatus.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newNewsStatus);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteNewsStatus(Guid id)
        {
            _appDBContext.NewsStatuses.Remove(await _appDBContext.NewsStatuses.FirstOrDefaultAsync(ns => ns.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<NewsStatusVM> TakeNewsStatusById(Guid id)
        {
            var newsStatus = _mapper.Map<NewsStatusVM>(await _appDBContext.NewsStatuses.AsNoTracking().FirstOrDefaultAsync(ns => ns.Id == id));

            return newsStatus;
        }

        public async Task<List<NewsStatusVM>> TakeNewsStatuses()
        {
            var newsStatusVMs = _mapper.Map<List<NewsStatusVM>>(await _appDBContext.NewsStatuses.AsNoTracking().ToListAsync());

            return newsStatusVMs;
        }

        public async Task UpdateNewsStatus(NewsStatusVM updatedNewsStatus)
        {
            var newsStatus = await _appDBContext.NewsStatuses.FirstOrDefaultAsync(ns => ns.Id == updatedNewsStatus.Id);

            if (newsStatus != null)
            {
                newsStatus.Title = updatedNewsStatus.Title;
                newsStatus.Description = updatedNewsStatus.Description;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
