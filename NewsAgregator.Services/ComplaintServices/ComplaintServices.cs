using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.Mapper.DataMappers;
using NewsAgregator.Mapper.PropertiesMappers;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.ComplaintServices
{
    public class ComplaintServices : IComplaintServices
    {
        private readonly AppDBContext _appDBContext;
        public ComplaintServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<ComplaintParameters> GetComplaintParametersAsync()
        {
            var complaintParameters = new ComplaintParameters()
            {
                Comments = (await _appDBContext.Comments.ToListAsync()).Select(c => ComplaintParametersMapper.CommentToParameter(c)).ToList(),
                Newses = (await _appDBContext.Newses.ToListAsync()).Select(n => ComplaintParametersMapper.NewsToParameter(n)).ToList(),
                ComplaintStatuses = (await _appDBContext.ComplaintStatuses.ToListAsync()).Select(cs => ComplaintParametersMapper.ComplaintStatusToParameter(cs)).ToList(),
                ComplaintTypes = (await _appDBContext.ComplaintTypes.ToListAsync()).Select(ct => ComplaintParametersMapper.ComplaintTypeToParameter(ct)).ToList(),
                Accounts = (await _appDBContext.Accounts.ToListAsync()).Select(a => ComplaintParametersMapper.AccountToParameter(a)).ToList(),

            };
            return complaintParameters;

        }

        public async Task AddComplaintAsync(ComplaintVM complaintVM)
        {
            var newComplaint = ComplaintMapper.ComplaintVMToComplaint(complaintVM);
            newComplaint.Id = Guid.NewGuid();
            if (complaintVM.ComplaintStatusId == null)
            {
                newComplaint.ComplaintStatusId = await _appDBContext.ComplaintStatuses.AsNoTracking().Where(cs => cs.Title.Equals("Waiting")).Select(cs => cs.Id).FirstOrDefaultAsync();
            }


            await _appDBContext.AddAsync(newComplaint);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteComplaintAsync(Guid id)
        {
            _appDBContext.Complaints.Remove(await _appDBContext.Complaints.FirstOrDefaultAsync(c => c.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<ComplaintVM> TakeComplaintByIdAsync(Guid id)
        {
            var complaintVM = ComplaintMapper.ComplaintToComplaintVM(await _appDBContext.Complaints
                .AsNoTracking()
                .Include(c => c.Comment)
                .Include(n => n.News)
                .Include(cs => cs.ComplaintStatus)
                .Include(ct => ct.ComplaintType)
                .Include(u => u.User)
                .Include(a => a.Administrator)
                .FirstOrDefaultAsync(c => c.Id == id));

            var compalintParameters = await GetComplaintParametersAsync();

            complaintVM.Comments = compalintParameters.Comments;
            complaintVM.Newses = compalintParameters.Newses;
            complaintVM.ComplaintStatuses = compalintParameters.ComplaintStatuses;
            complaintVM.ComplaintTypes = compalintParameters.ComplaintTypes;
            complaintVM.Accounts = compalintParameters.Accounts;

            return complaintVM;
        }

        public async Task<List<ComplaintVM>> TakeComplaintsAsync()
        {

            var complaintVMs = (await _appDBContext.Complaints
                .AsNoTracking()
                .Include(c => c.Comment)
                .Include(n => n.News)
                .Include(cs => cs.ComplaintStatus)
                .Include(ct => ct.ComplaintType)
                .Include(u => u.User)
                .Include(a => a.Administrator)
                .ToListAsync()).Select(c => ComplaintMapper.ComplaintToComplaintVM(c)).ToList();

            return complaintVMs;
        }

        public async Task UpdateComplaintAsync(ComplaintVM updatedComplaintVM)
        {
            var complaint = await _appDBContext.Complaints.FirstOrDefaultAsync(accs => accs.Id == updatedComplaintVM.Id);

            if (complaint != null)
            {
                complaint.Title = updatedComplaintVM.Title;
                complaint.Text = updatedComplaintVM.Text;
                complaint.CommentId = updatedComplaintVM.CommentId;
                complaint.NewsId= updatedComplaintVM.NewsId;
                complaint.ComplaintStatusId = updatedComplaintVM.ComplaintStatusId;
                if (updatedComplaintVM.ComplaintStatusId == null)
                {
                    complaint.ComplaintStatusId = await _appDBContext.ComplaintStatuses.AsNoTracking().Where(cs => cs.Title.Equals("Waiting")).Select(cs => cs.Id).FirstOrDefaultAsync();
                }
                complaint.ComplaintTypeId = updatedComplaintVM.ComplaintTypeId;
                complaint.UserId = updatedComplaintVM.UserId;
                complaint.AdministratorId = updatedComplaintVM.AdministratorId;

                await _appDBContext.SaveChangesAsync();
            }
        }
    }
}
