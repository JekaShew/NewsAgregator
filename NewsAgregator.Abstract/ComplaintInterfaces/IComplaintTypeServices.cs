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
        public Task<List<ComplaintTypeVM>> TakeComplaintTypes();

        public Task<ComplaintTypeVM> TakeComplaintTypeById(Guid id);

        public Task AddComplaintType(ComplaintTypeVM complaintType);

        public Task DeleteComplaintType(Guid id);

        public Task UpdateComplaintType(ComplaintTypeVM updatedComplaintType);
    }
}
