using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Additional
{
    public class ComplaintParameters
    {
        public List<Parameter>? Comments { get; set; }
        public List<Parameter>? Newses { get; set; }
        public List<Parameter>? ComplaintStatuses { get; set; }
        public List<Parameter>? ComplaintTypes { get; set; }
        public List<Parameter>? Accounts { get; set; }
    }
}
