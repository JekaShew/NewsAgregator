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
    public class ComplaintServices : IComplaintServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;
        public ComplaintServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
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
