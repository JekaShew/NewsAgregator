using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Additional
{
    public  class ForgotPasswordModel
    {
        public string Login { get; set; }
        public string SecretWord { get; set; }
        public string NewPassword { get; set; }
    }
}
