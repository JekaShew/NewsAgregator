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
        public Task<List<NewsVM>> TakeNewses();

        public Task<NewsVM> TakeNewsById(Guid id);

        public Task AddNews(NewsVM news);

        public Task DeleteNews(Guid id);

        public Task UpdateNews(NewsVM updatedNews);
        public Task<NewsParameters> GetNewsParameters();
    }
}
