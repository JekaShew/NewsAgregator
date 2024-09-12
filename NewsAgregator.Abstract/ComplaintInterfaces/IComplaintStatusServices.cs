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
        public Task<List<ComplaintStatusVM>> TakeComplaintStatusesAsync();

        public Task<ComplaintStatusVM> TakeComplaintStatusByIdAsync(Guid id);

        public Task AddComplaintStatusAsync(ComplaintStatusVM complaintStatus);

        public Task DeleteComplaintStatusAsync(Guid id);

        public Task UpdateComplaintStatusAsync(ComplaintStatusVM updatedComplaintStatus);
    }
}
