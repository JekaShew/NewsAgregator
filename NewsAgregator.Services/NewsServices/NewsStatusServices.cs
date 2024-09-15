using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Mappers;
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
        public NewsStatusServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task AddNewsStatusAsync(NewsStatusVM newsStatusVM)
        {
            var newNewsStatus =NewsStatusMapper.NewsStatusVMToNewsStatus(newsStatusVM);
            newNewsStatus.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newNewsStatus);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteNewsStatusAsync(Guid id)
        {
            _appDBContext.NewsStatuses.Remove(await _appDBContext.NewsStatuses.FirstOrDefaultAsync(ns => ns.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<NewsStatusVM> TakeNewsStatusByIdAsync(Guid id)
        {
            var newsStatusVM = NewsStatusMapper.NewsStatusToNewsStatusVM(await _appDBContext.NewsStatuses.AsNoTracking().FirstOrDefaultAsync(ns => ns.Id == id));

            return newsStatusVM;
        }

        public async Task<List<NewsStatusVM>> TakeNewsStatusesAsync()
        {
            var newsStatusVMs = (await _appDBContext.NewsStatuses.AsNoTracking().ToListAsync()).Select(ns => NewsStatusMapper.NewsStatusToNewsStatusVM(ns)).ToList();

            return newsStatusVMs;
        }

        public async Task UpdateNewsStatusAsync(NewsStatusVM updatedNewsStatusVM)
        {
            var newsStatus = await _appDBContext.NewsStatuses.FirstOrDefaultAsync(ns => ns.Id == updatedNewsStatusVM.Id);

            if (newsStatus != null)
            {
                newsStatus.Title = updatedNewsStatusVM.Title;
                newsStatus.Description = updatedNewsStatusVM.Description;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
