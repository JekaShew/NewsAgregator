using AutoMapper;
using Mapper.Mappers.PropertiesMappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.Mapper.DataMappers;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.AccountServices
{
    public class AccountServices : IAccountServices
    {
        private readonly AppDBContext _appDBContext;
        public AccountServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<AccountParameters> GetAccountParametersAsync()
        {
            var accountParameters = new AccountParameters()
            {
                AccountStatuses = (await _appDBContext.AccountStatuses.ToListAsync()).Select(ac => AccountParametersMapper.AccountStatusToParameter(ac)).ToList(),
                Roles = (await _appDBContext.Roles.ToListAsync()).Select(r => AccountParametersMapper.RoleToParameter(r)).ToList(),
            };
            return accountParameters;
        }

        public async Task AddAccountAsync(AccountVM accountVM)
        {
            var newAccount = AccountMapper.AccountVMToAccount(accountVM);

            newAccount.Id = Guid.NewGuid();
            newAccount.SecurityStamp = await GetHashAsync(accountVM.SecretWord);
            newAccount.PasswordHash = await GetPasswordHashAsync(accountVM.Password, await GetHashAsync(accountVM.SecretWord));

            await _appDBContext.Accounts.AddAsync(newAccount);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            _appDBContext.Accounts.Remove(await _appDBContext.Accounts.FirstOrDefaultAsync(acc => acc.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<AccountVM> TakeAccountByIdAsync(Guid id)
        {
            var account = AccountMapper.AccountToAccountVM(await _appDBContext.Accounts
                .AsNoTracking()
                .Include(acc => acc.AccountStatus)
                .Include(acc => acc.Role)
                .FirstOrDefaultAsync(acc => acc.Id == id));

            var accountParameters = await GetAccountParametersAsync();
            account.AccountStatuses = accountParameters.AccountStatuses;
            account.Roles = accountParameters.Roles;

            return account;
        }



        public async Task<List<AccountVM>> TakeAccountsAsync()
        {
            var accountVMs = (await _appDBContext.Accounts
                .AsNoTracking()
                .Include(acc => acc.AccountStatus)
                .Include(acc => acc.Role)
                .ToListAsync())
                    .Select(a => AccountMapper.AccountToAccountVM(a)).ToList();

            return accountVMs;
        }

        public async Task UpdateAccountAsync(AccountVM updatedAccountVM)
        {
            var account = await _appDBContext.Accounts.FirstOrDefaultAsync(pc => pc.Id == updatedAccountVM.Id);
            //account = AccountMapper.AccountVMToAccount(updatedAccountVM);

            await _appDBContext.SaveChangesAsync();
            if (account != null)
            {
                account.UserName = updatedAccountVM.UserName;
                account.Login = updatedAccountVM.Login;
                //account.PasswordHash = updatedAccountVM.Password;
                account.FIO = updatedAccountVM.FIO;
                account.Email = updatedAccountVM.Email;
                account.Phone = updatedAccountVM.Phone;
                account.DesiredNewsRating = updatedAccountVM.DesiredNewsRating;
                account.AccountStatusId = updatedAccountVM.AccountStatusId;
                account.RoleId = updatedAccountVM.RoleId;

                await _appDBContext.SaveChangesAsync();
            }
        }
        private async Task<string> GetPasswordHashAsync(string password, string securityStamp)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes($"{password}{securityStamp}");
                var ms = new MemoryStream(inputBytes);
                var hashBytes = await md5.ComputeHashAsync(ms);
                var passwordHash = Encoding.UTF8.GetString(hashBytes);
                return passwordHash;
            }
        }

        private async Task<string> GetHashAsync(string stringToHash)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes($"{stringToHash}");
                var ms = new MemoryStream(inputBytes);
                var hashBytes = await md5.ComputeHashAsync(ms);
                var passwordHash = Encoding.UTF8.GetString(hashBytes);
                return passwordHash;
            }
        }

        public async Task<bool> CheckIsLoginRegisteredAsync(string login)
        {
            return await _appDBContext.Accounts.AnyAsync(a => a.Login.Equals(login));
        }

        public async Task<bool> CheckPasswordAsync(string login, string password)
        {
            var account = await _appDBContext.Accounts.Where(a => a.Login == login).FirstOrDefaultAsync();
            var enteredPasswordHash = await GetPasswordHashAsync(password, account.SecurityStamp);

            return await _appDBContext.Accounts.AnyAsync(a => a.Login.Equals(account.Login) && a.PasswordHash.Equals(enteredPasswordHash));
        }

        public async Task<bool> ChangePasswordAsync(string login, string secretWord)
        {
            throw new NotImplementedException();
        }
    }
}
