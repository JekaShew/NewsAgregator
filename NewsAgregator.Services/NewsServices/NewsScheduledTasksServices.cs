using NewsAgregator.Abstract.NewsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.NewsServices
{
    public class NewsScheduledTasksServices : INewsShcheduledTasksServices
    {
        private readonly INewsServices _newsServices;
        public NewsScheduledTasksServices(INewsServices newsServices)
        {
                _newsServices = newsServices;
        }
        public async Task DeleteNewsWithBadRateAsync()
        {
            var allNewses = await _newsServices.TakeNewsesAsync();
            var toDeleteNewses = allNewses.Where(n => n.PositiveRating <= 4).ToList();
            foreach (var news in toDeleteNewses)
            {
                await _newsServices.DeleteNewsAsync(news.Id);
            }

            //await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteOldNewsesAsync()
        {
            var allNewses = await _newsServices.TakeNewsesAsync();
            TimeSpan twoWeeks = TimeSpan.FromDays(14);
            var oldNewses = allNewses.Where(n => n.Date.Value >= DateTime.Now.AddDays(-14)).ToList();
            foreach (var news in oldNewses)
            {
                await _newsServices.DeleteNewsAsync(news.Id);
            }

            //await _appDBContext.SaveChangesAsync();
        }
    }
}
