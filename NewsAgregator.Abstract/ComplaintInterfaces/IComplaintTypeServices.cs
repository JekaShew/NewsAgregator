using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.ComplaintInterfaces
{
    public interface IComplaintTypeServices
    {
        public Task<List<ComplaintTypeVM>> TakeComplaintTypesAsync();

        public Task<ComplaintTypeVM> TakeComplaintTypeByIdAsync(Guid id);

        public Task AddComplaintTypeAsync(ComplaintTypeVM complaintType);

        public Task DeleteComplaintTypeAsync(Guid id);

        public Task UpdateComplaintTypeAsync(ComplaintTypeVM updatedComplaintType);
    }
}
