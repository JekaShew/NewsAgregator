using Microsoft.AspNetCore.Mvc;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.AccountInterfaces
{
    public interface IAuthorizationServices
    {
        public Task<IActionResult> SignInAsync(CreateAccountVM accountVM);
        public Task<IActionResult> SignUpAsync(CreateAccountVM accountVM);

        public Task<IActionResult> SignOutAsync(Guid id);

    }
}

