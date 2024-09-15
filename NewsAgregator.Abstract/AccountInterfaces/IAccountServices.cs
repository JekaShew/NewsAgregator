﻿using NewsAgregator.ViewModels.Additional;
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
        public Task<List<AccountVM>> TakeAccountsAsync();

        public Task<AccountVM> TakeAccountByIdAsync(Guid id);

        public Task AddAccountAsync(AccountVM account);

        public Task DeleteAccountAsync(Guid id);

        public Task UpdateAccountAsync(AccountVM updatedAccount);
        public Task<AccountParameters> GetAccountParametersAsync();
        public Task<bool> CheckIsLoginRegisteredAsync(string login);
        public Task<bool> CheckPasswordAsync(string login, string password);
        public Task<bool> ChangePasswordAsync(string login, string secretWord);

    }
}
