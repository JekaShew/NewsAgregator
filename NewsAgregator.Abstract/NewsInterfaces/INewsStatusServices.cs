using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.NewsInterfaces
{
    public interface INewsStatusServices
    {
        public Task<List<NewsStatusVM>> TakeNewsStatusesAsync();

        public Task<NewsStatusVM> TakeNewsStatusByIdAsync(Guid id);

        public Task AddNewsStatusAsync(NewsStatusVM newsStatus);

        public Task DeleteNewsStatusAsync(Guid id);

        public Task UpdateNewsStatusAsync(NewsStatusVM updatedNewsStatus);
    }
}
