using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Mapper.DataMappers
{
    [Mapper]
    public static partial class ComplaintStatusMapper
    {
        public static partial ComplaintStatusVM? ComplaintStatusToComplaintStatusVM(ComplaintStatus? complaintStatus);

        public static partial ComplaintStatus? ComplaintStatusVMToComplaintStatus(ComplaintStatusVM? complaintStatusVM);

    }
}
