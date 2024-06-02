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
    public class ComplaintStatusServices : IComplaintStatusServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;
        public ComplaintStatusServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }
        public async Task AddComplaintStatus(ComplaintStatusVM complaintStatus)
        {
            var newComplaintStatus = _mapper.Map<Data.Models.ComplaintStatus>(complaintStatus);
            newComplaintStatus.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newComplaintStatus);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteComplaintStatus(Guid id)
        {
            _appDBContext.ComplaintStatuses.Remove(await _appDBContext.ComplaintStatuses.FirstOrDefaultAsync(cs => cs.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<ComplaintStatusVM> TakeComplaintStatusById(Guid id)
        {
            var complaintStatus = _mapper.Map<ComplaintStatusVM>(await _appDBContext.ComplaintStatuses.AsNoTracking().FirstOrDefaultAsync(cs => cs.Id == id));

            return complaintStatus;
        }

        public async Task<List<ComplaintStatusVM>> TakeComplaintStatuses()
        {
            var complaintStatusVMs = _mapper.Map<List<ComplaintStatusVM>>(await _appDBContext.ComplaintStatuses.AsNoTracking().ToListAsync());

            return complaintStatusVMs;
        }

        public async Task UpdateComplaintStatus(ComplaintStatusVM updatedComplaintStatus)
        {
            var complaintStatus = await _appDBContext.ComplaintStatuses.FirstOrDefaultAsync(cs => cs.Id == updatedComplaintStatus.Id);

            if (complaintStatus != null)
            {
                complaintStatus.Title = updatedComplaintStatus.Title;
                complaintStatus.Description = updatedComplaintStatus.Description;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
