using NewsAgregator.ViewModels.Additional;
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
        public Task<List<ComplaintVM>> TakeComplaintsAsync();

        public Task<ComplaintVM> TakeComplaintByIdAsync(Guid id);

        public Task AddComplaintAsync(ComplaintVM complaint);

        public Task DeleteComplaintAsync(Guid id);

        public Task UpdateComplaintAsync(ComplaintVM updatedComplaint);
        public Task<ComplaintParameters> GetComplaintParametersAsync();
    }
}
