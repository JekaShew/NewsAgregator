using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.ComplaintInterfaces
{
    public interface IComplaintServices
    {
        public Task<List<ComplaintVM>> TakeComplaints();

        public Task<ComplaintVM> TakeComplaintById(Guid id);

        public Task AddComplaint(ComplaintVM complaint);

        public Task DeleteComplaint(Guid id);

        public Task UpdateComplaint(ComplaintVM updatedComplaint);
    }
}
