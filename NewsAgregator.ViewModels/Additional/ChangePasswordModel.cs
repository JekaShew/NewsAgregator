using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Additional
{
    public class ChangePasswordModel
    {
        public string Login { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
