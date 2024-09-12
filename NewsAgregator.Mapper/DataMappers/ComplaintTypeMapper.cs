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
    public static partial class ComplaintTypeMapper
    {
        public static partial ComplaintTypeVM? ComplaintTypeToComplaintTypeVM(ComplaintType? complaintType);

        public static partial ComplaintType? ComplaintTypeVMToComplaintType(ComplaintTypeVM? complaintTypeVM);

    }
}
