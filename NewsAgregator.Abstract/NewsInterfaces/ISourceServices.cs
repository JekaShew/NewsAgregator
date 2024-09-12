using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.NewsInterfaces
{
    public interface ISourceServices
    {
        public Task<List<SourceVM>> TakeSourcesAsync();

        public Task<SourceVM> TakeSourceByIdAsync(Guid id);

        public Task DeleteSourceAsync(Guid id);

        public Task UpdateSourceAsync(SourceVM updatedSource);
    }
}
