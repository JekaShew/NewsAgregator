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
        public Task<List<NewsStatusVM>> TakeNewsStatuses();

        public Task<NewsStatusVM> TakeNewsStatusById(Guid id);

        public Task AddNewsStatus(NewsStatusVM newsStatus);

        public Task DeleteNewsStatus(Guid id);

        public Task UpdateNewsStatus(NewsStatusVM updatedNewsStatus);
    }
}
