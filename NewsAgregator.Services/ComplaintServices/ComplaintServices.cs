using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
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
        private readonly IMapper _mapper;
        public ComplaintServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }

        public async Task<ComplaintParameters> GetComplaintParameters()
        {
            var complaintParameters = new ComplaintParameters()
            {
                Comments = await _appDBContext.Comments.Select(accs => new Parameter { Id = accs.Id, Text = accs.Text }).ToListAsync(),
                Newses = await _appDBContext.Newses.Select(accs => new Parameter { Id = accs.Id, Text = accs.Title }).ToListAsync(),
                ComplaintStatuses = await _appDBContext.ComplaintStatuses.Select(accs => new Parameter { Id = accs.Id, Text = accs.Title }).ToListAsync(),
                ComplaintTypes = await _appDBContext.ComplaintTypes.Select(accs => new Parameter { Id = accs.Id, Text = accs.Title }).ToListAsync(),
                Accounts = await _appDBContext.Accounts.Select(accs => new Parameter { Id = accs.Id, Text = accs.UserName }).ToListAsync(),

            };
            return complaintParameters;

        }

        public async Task ConvertComplaintParameters(ComplaintVM complaintVM)
        {
            var comment = await _appDBContext.Comments
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == complaintVM.CommentId);
            var news = await _appDBContext.Newses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == complaintVM.NewsId);
            var complaintStatus = await _appDBContext.ComplaintStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == complaintVM.ComplaintStatusId);
            var complaintType = await _appDBContext.ComplaintTypes
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == complaintVM.ComplaintTypeId);
            var user = await _appDBContext.Accounts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == complaintVM.UserId);
            var administrator = await _appDBContext.Accounts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == complaintVM.AdministratorId);

            complaintVM.FromDataModel(comment, news, complaintStatus, complaintType, user, administrator);
        }

        public async Task AddComplaint(ComplaintVM complaint)
        {
            var newComplaint = _mapper.Map<Data.Models.Complaint>(complaint);
            newComplaint.Id = Guid.NewGuid();
            newComplaint.Comment = null;
            newComplaint.News = null;
            newComplaint.ComplaintStatus = null;
            newComplaint.ComplaintType = null;
            newComplaint.User = null;
            newComplaint.Administrator = null;

            await _appDBContext.AddAsync(newComplaint);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteComplaint(Guid id)
        {
            _appDBContext.Complaints.Remove(await _appDBContext.Complaints.FirstOrDefaultAsync(c => c.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<ComplaintVM> TakeComplaintById(Guid id)
        {
            var complaint = _mapper.Map<ComplaintVM>(await _appDBContext.Complaints
                .AsNoTracking()
                .Include(c => c.Comment)
                .Include(n => n.News)
                .Include(cs => cs.ComplaintStatus)
                .Include(ct => ct.ComplaintType)
                .Include(u => u.User)
                .Include(a => a.Administrator)
                .FirstOrDefaultAsync(c => c.Id == id));

            var compalintParameters = await GetComplaintParameters();
            await ConvertComplaintParameters(complaint);

            complaint.Comments = compalintParameters.Comments;
            complaint.Newses = compalintParameters.Newses;
            complaint.ComplaintStatuses = compalintParameters.ComplaintStatuses;
            complaint.ComplaintTypes = compalintParameters.ComplaintTypes;
            complaint.Accounts = compalintParameters.Accounts;

            return complaint;
        }

        public async Task<List<ComplaintVM>> TakeComplaints()
        {

            var complaintVMs = _mapper.Map<List<ComplaintVM>>(await _appDBContext.Complaints
                .AsNoTracking()
                .Include(c => c.Comment)
                .Include(n => n.News)
                .Include(cs => cs.ComplaintStatus)
                .Include(ct => ct.ComplaintType)
                .Include(u => u.User)
                .Include(a => a.Administrator)
                .ToListAsync());

            foreach (var complaintVM in complaintVMs)
            {
                await ConvertComplaintParameters(complaintVM);
            }

            return complaintVMs;
        }

        public async Task UpdateComplaint(ComplaintVM updatedComplaint)
        {
            var complaint = await _appDBContext.Complaints.FirstOrDefaultAsync(accs => accs.Id == updatedComplaint.Id);

            if (complaint != null)
            {
                complaint.Title = updatedComplaint.Title;
                complaint.Text = updatedComplaint.Text;
                complaint.CommentId = updatedComplaint.CommentId;
                complaint.Comment = null;
                complaint.NewsId= updatedComplaint.NewsId;
                complaint.News = null;
                complaint.ComplaintStatusId = updatedComplaint.ComplaintStatusId;
                complaint.ComplaintStatus = null;
                complaint.ComplaintTypeId = updatedComplaint.ComplaintTypeId;
                complaint.ComplaintType = null;
                complaint.UserId = updatedComplaint.UserId;
                complaint.User = null;
                complaint.AdministratorId = updatedComplaint.AdministratorId;
                complaint.Administrator = null;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
