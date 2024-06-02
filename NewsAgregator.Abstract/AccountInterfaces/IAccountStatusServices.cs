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
        
        public Task<List<AccountStatusVM>> TakeAccountStatuses();

        public Task<AccountStatusVM> TakeAccountStatusById(Guid id);

        public Task AddAccountStatus(AccountStatusVM accountStatus);

        public Task DeleteAccountStatus(Guid id);

        public Task UpdateAccountStatus(AccountStatusVM updatedAccountStatus);
    }
}
