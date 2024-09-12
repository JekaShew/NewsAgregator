using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.AccountServices
{
    internal class AuthorizationServices : IAuthorizationServices
    {
        public Task<IActionResult> SignInAsync(AccountVM accountVM)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> SignOutAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> SignUpAsync(AccountVM accountVM)
        {
            throw new NotImplementedException();
        }
    }
}
