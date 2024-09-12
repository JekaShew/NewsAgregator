using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.AccountInterfaces
{
    public interface IAccountStatusServices
    {

        public Task<List<AccountStatusVM>> TakeAccountStatusesAsync();

        public Task<AccountStatusVM> TakeAccountStatusByIdAsync(Guid id);

        public Task AddAccountStatusAsync(AccountStatusVM accountStatus);

        public Task DeleteAccountStatusAsync(Guid id);

        public Task UpdateAccountStatusAsync(AccountStatusVM updatedAccountStatus);
    }
}
