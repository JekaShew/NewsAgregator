using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.AccountInterfaces
{
    public interface IAccountServices
    {
        public Task<List<AccountVM>> TakeAccounts();

        public Task<AccountVM> TakeAccountById(Guid id);

        public Task AddAccount(AccountVM account);

        public Task DeleteAccount(Guid id);

        public Task UpdateAccount(AccountVM updatedAccount);
    }
}
