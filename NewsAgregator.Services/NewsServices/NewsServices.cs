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
    public class NewsServices : INewsServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;
        public NewsServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }
        public async Task AddNews(NewsVM news)
        {
            var newNews = _mapper.Map<Data.Models.News>(news);
            newNews.Id = Guid.NewGuid();
            newNews.NewsStatus = null;

            await _appDBContext.AddAsync(newNews);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteNews(Guid id)
        {
            _appDBContext.Newses.Remove(await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<NewsVM> TakeNewsById(Guid id)
        {
            var news = _mapper.Map<NewsVM>(await _appDBContext.Newses
                .AsNoTracking()
                .Include(ns => ns.NewsStatus)
                .FirstOrDefaultAsync(n => n.Id == id));

            return news;
        }

        public async Task<List<NewsVM>> TakeNewses()
        {
            var newsVMs = _mapper.Map<List<NewsVM>>(await _appDBContext.Newses
                .AsNoTracking()
                .Include(ns => ns.NewsStatus)
                .ToListAsync());

            return newsVMs;
        }

        public async Task UpdateNews(NewsVM updatedNews)
        {
            var news = await _appDBContext.Newses.FirstOrDefaultAsync(n => n.Id == updatedNews.Id);

            if (news != null)
            {
                news.Title = updatedNews.Title;
                news.Text = updatedNews.Text;
                news.Date = updatedNews.Date;
                news.PositiveRating = updatedNews.PositiveRating;
                news.NewsStatusId = updatedNews.NewsStatusId;
                news.NewsStatus = null;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
