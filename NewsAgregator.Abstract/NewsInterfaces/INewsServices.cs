using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.NewsInterfaces
{
    public interface INewsServices
    {
        public Task<List<NewsVM>> TakeNewsesAsync();
        public Task<List<NewsVM>> TakeTopNewsesAsync();
        public Task<List<NewsVM>> TakeSuitableNewsesAsync();

        public Task<NewsVM> TakeNewsByIdAsync(Guid id);

        public Task AddNewsAsync(NewsVM news);
        public Task AggregateNewsAsync();

        public Task DeleteNewsAsync(Guid id);

        public Task UpdateNewsAsync(NewsVM updatedNews);
        public Task<NewsParameters> GetNewsParametersAsync();

        public Task UpdateNewsRateAsync();
    }
}
