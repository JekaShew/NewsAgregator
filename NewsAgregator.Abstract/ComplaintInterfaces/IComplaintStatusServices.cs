using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.ComplaintInterfaces
{
    public interface IComplaintStatusServices
    {
        public Task<List<ComplaintStatusVM>> TakeComplaintStatuses();

        public Task<ComplaintStatusVM> TakeComplaintStatusById(Guid id);

        public Task AddComplaintStatus(ComplaintStatusVM complaintStatus);

        public Task DeleteComplaintStatus(Guid id);

        public Task UpdateComplaintStatus(ComplaintStatusVM updatedComplaintStatus);
    }
}
