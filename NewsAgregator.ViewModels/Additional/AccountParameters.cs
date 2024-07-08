using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.ViewModels.Additional
{
    public class AccountParameters
    {
        public List<Parameter>? AccountStatuses { get; set; }
        public List<Parameter>? Roles { get; set; }
    }
}
