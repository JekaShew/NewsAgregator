using NewsAgregator.ViewModels.Additional;
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
        public Task<Guid?> TakeAccountIdByLoginAsync(string login);
        public Task<AccountVM> TakeAccountByRTokenIdAsync(Guid refreshTokenId);
        public Task AddAccountAsync(CreateAccountVM account);

        public Task DeleteAccountAsync(Guid id);

        public Task UpdateAccountAsync(AccountVM updatedAccount);
        public Task<AccountParameters> GetAccountParametersAsync();
        public Task<bool> CheckIsLoginRegisteredAsync(string login);
        public Task<bool> CheckLoginPasswordAsync(string login, string password);
        public Task<bool> CanChangePasswordAsync(string login, string password);
        public Task<bool> CanChangeForgottenPasswordAsync(string login, string secretWord);

        public Task ChangePasswordAsync(string password);

        public Guid? GetCurrentAccountId();
    }
}
