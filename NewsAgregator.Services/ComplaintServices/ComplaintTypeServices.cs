using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.ComplaintServices
{
    public class ComplaintTypeServices : IComplaintTypeServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;
        public ComplaintTypeServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }
        public async Task AddComplaintType(ComplaintTypeVM complaintType)
        {
            var newComplaintType = _mapper.Map<Data.Models.ComplaintType>(complaintType);
            newComplaintType.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newComplaintType);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteComplaintType(Guid id)
        {
            _appDBContext.ComplaintTypes.Remove(await _appDBContext.ComplaintTypes.FirstOrDefaultAsync(ct => ct.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<ComplaintTypeVM> TakeComplaintTypeById(Guid id)
        {
            var complaintType = _mapper.Map<ComplaintTypeVM>(await _appDBContext.ComplaintTypes.AsNoTracking().FirstOrDefaultAsync(ct => ct.Id == id));

            return complaintType;
        }

        public async Task<List<ComplaintTypeVM>> TakeComplaintTypes()
        {
            var complaintTypeVMs = _mapper.Map<List<ComplaintTypeVM>>(await _appDBContext.ComplaintTypes.AsNoTracking().ToListAsync());

            return complaintTypeVMs;
        }

        public async Task UpdateComplaintType(ComplaintTypeVM updatedComplaintType)
        {
            var complaintType = await _appDBContext.ComplaintTypes.FirstOrDefaultAsync(ct => ct.Id == updatedComplaintType.Id);

            if (complaintType != null)
            {
                complaintType.Title = updatedComplaintType.Title;
                complaintType.Description = updatedComplaintType.Description;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
