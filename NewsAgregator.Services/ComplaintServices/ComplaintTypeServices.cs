using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.Mapper.DataMappers;
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
        public ComplaintTypeServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task AddComplaintTypeAsync(ComplaintTypeVM complaintType)
        {
            var newComplaintType = ComplaintTypeMapper.ComplaintTypeVMToComplaintType(complaintType);
            newComplaintType.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newComplaintType);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteComplaintTypeAsync(Guid id)
        {
            _appDBContext.ComplaintTypes.Remove(await _appDBContext.ComplaintTypes.FirstOrDefaultAsync(ct => ct.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<ComplaintTypeVM> TakeComplaintTypeByIdAsync(Guid id)
        {
            var complaintTypeVM = ComplaintTypeMapper.ComplaintTypeToComplaintTypeVM(await _appDBContext.ComplaintTypes.AsNoTracking().FirstOrDefaultAsync(ct => ct.Id == id));

            return complaintTypeVM;
        }

        public async Task<List<ComplaintTypeVM>> TakeComplaintTypesAsync()
        {
            var complaintTypeVMs = (await _appDBContext.ComplaintTypes.AsNoTracking().ToListAsync()).Select(ct => ComplaintTypeMapper.ComplaintTypeToComplaintTypeVM(ct)).ToList();

            return complaintTypeVMs;
        }

        public async Task UpdateComplaintTypeAsync(ComplaintTypeVM updatedComplaintType)
        {
            var complaintType = await _appDBContext.ComplaintTypes.FirstOrDefaultAsync(ct => ct.Id == updatedComplaintType.Id);

            if (complaintType != null)
            {
                complaintType.Title = updatedComplaintType.Title;
                complaintType.Description = updatedComplaintType.Description;

                await _appDBContext.SaveChangesAsync();
            }
        }
    }
}
