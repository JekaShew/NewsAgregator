using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Mappers;
using NewsAgregator.Mapper.DataMappers;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.NewsServices
{
     public class SourceServices : ISourceServices
    {
        private readonly AppDBContext _appDBContext;
        public SourceServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }


        public async Task DeleteSourceAsync(Guid id)
        {
            _appDBContext.Sources.Remove(await _appDBContext.Sources.FirstOrDefaultAsync(s => s.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<SourceVM> TakeSourceByIdAsync(Guid id)
        {
            var sourceVM = SourceMapper.SourceToSourceVM(await _appDBContext.Sources.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id));

            return sourceVM;
        }

        public async Task<List<SourceVM>> TakeSourcesAsync()
        {
            var sourceVMs = (await _appDBContext.Sources.AsNoTracking().ToListAsync())
                                                .Select(s => SourceMapper.SourceToSourceVM(s)).ToList();

            return sourceVMs;
        }

        public async Task UpdateSourceAsync(SourceVM updatedSourceVM)
        {
            var source = await _appDBContext.Sources.FirstOrDefaultAsync(s => s.Id == updatedSourceVM.Id);

            if (source != null)
            {
                source.Title = updatedSourceVM.Title;
                source.BaseUrl = updatedSourceVM.BaseUrl;
                source.RssUrl = updatedSourceVM.RssUrl;

                await _appDBContext.SaveChangesAsync();
            }
        }
    }
