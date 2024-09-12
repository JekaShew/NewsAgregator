using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Mappers;
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
        public ComplaintStatusServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task AddComplaintStatusAsync(ComplaintStatusVM complaintStatusVM)
        {
            var newComplaintStatus = ComplaintStatusMapper.ComplaintStatusVMToComplaintStatus(complaintStatusVM);
            newComplaintStatus.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newComplaintStatus);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteComplaintStatusAsync(Guid id)
        {
            _appDBContext.ComplaintStatuses.Remove(await _appDBContext.ComplaintStatuses.FirstOrDefaultAsync(cs => cs.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<ComplaintStatusVM> TakeComplaintStatusByIdAsync(Guid id)
        {
            var complaintStatus = ComplaintStatusMapper.ComplaintStatusToComplaintStatusVM(await _appDBContext.ComplaintStatuses.AsNoTracking().FirstOrDefaultAsync(cs => cs.Id == id));

            return complaintStatus;
        }

        public async Task<List<ComplaintStatusVM>> TakeComplaintStatusesAsync()
        {
            var complaintStatusVMs = (await _appDBContext.ComplaintStatuses.AsNoTracking().ToListAsync())
                                                .Select(cs => ComplaintStatusMapper.ComplaintStatusToComplaintStatusVM(cs)).ToList();

            return complaintStatusVMs;
        }

        public async Task UpdateComplaintStatusAsync(ComplaintStatusVM updatedComplaintStatusVM)
        {
            var complaintStatus = await _appDBContext.ComplaintStatuses.FirstOrDefaultAsync(cs => cs.Id == updatedComplaintStatusVM.Id);
            //complaintStatus = ComplaintStatusMapper.ComplaintStatusVMToComplaintStatus(updatedComplaintStatusVM);

            if (complaintStatus != null)
            {
                complaintStatus.Title = updatedComplaintStatusVM.Title;
                complaintStatus.Description = updatedComplaintStatusVM.Description;

                await _appDBContext.SaveChangesAsync();
            }


        }
    }
}
