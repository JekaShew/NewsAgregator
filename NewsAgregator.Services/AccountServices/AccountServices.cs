using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.AccountServices
{
    public class AccountServices : IAccountServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;
        public AccountServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }

        public async Task<AccountParameters> GetAccountParameters()
        {
            var accountParameters = new AccountParameters()
            {
                AccountStatuses = await _appDBContext.AccountStatuses.Select(accs => new Parameter { Id = accs.Id, Text = accs.Title }).ToListAsync(),
                Roles = await _appDBContext.Roles.Select(r => new Parameter { Id = r.Id, Text = r.Title }).ToListAsync()
            }; 
            return accountParameters;

        }

        public async Task ConvertAccountParameters(AccountVM accountVM)
        {
            var accountStatus = await _appDBContext.AccountStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == accountVM.AccountStatusId);
            var role = await _appDBContext.Roles
                        .AsNoTracking()
                        .FirstOrDefaultAsync(r => r.Id == accountVM.RoleId);
            accountVM.FromDataModel(accountStatus, role);

        }

        public async Task AddAccount(AccountVM account)
        {
            var newAccount = _mapper.Map<Data.Models.Account>(account);
            newAccount.Id = Guid.NewGuid();
            newAccount.AccountStatusId = account.AccountStatusId;
            newAccount.RoleId = account.RoleId;
            newAccount.AccountStatus = null;
            newAccount.Role = null;
                
            await _appDBContext.AddAsync(newAccount);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteAccount(Guid id)
        {
            _appDBContext.Accounts.Remove(await _appDBContext.Accounts.FirstOrDefaultAsync(acc => acc.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<AccountVM> TakeAccountById(Guid id)
        {
            var account = _mapper.Map<AccountVM>(await _appDBContext.Accounts
                .AsNoTracking()
                .Include(acc => acc.AccountStatus)
                .Include(acc => acc.Role)
                .FirstOrDefaultAsync(acc => acc.Id == id));

            var accountParameters = await GetAccountParameters();
            await ConvertAccountParameters(account);
            account.AccountStatuses = accountParameters.AccountStatuses;
            account.Roles = accountParameters.Roles;

            return account;
        }

        

        public async Task<List<AccountVM>> TakeAccounts()
        {
            var accountVMs = _mapper.Map<List<AccountVM>>(await _appDBContext.Accounts
                .AsNoTracking()
                .Include(acc => acc.AccountStatus)
                .Include(acc => acc.Role)
                .ToListAsync());

            foreach (var accountVM in accountVMs)
            {
                await ConvertAccountParameters(accountVM);
            }

                //var tasks = new List<Task>();
                //foreach(var accountVM in accountVMs)
                //{
                //    tasks.Add(ConvertAccountParameters(accountVM));
                //}
                //await Task.WhenAll(tasks);

                return accountVMs;
        }

        public async Task UpdateAccount(AccountVM updatedAccount)
        {
            var account = await _appDBContext.Accounts.FirstOrDefaultAsync(pc => pc.Id == updatedAccount.Id);

            if (account != null)
            {
                account.UserName = updatedAccount.UserName;
                account.Login = updatedAccount.Login;
                account.Password = updatedAccount.Password;
                account.FIO = updatedAccount.FIO;
                account.Email = updatedAccount.Email;
                account.Phone = updatedAccount.Phone;
                account.DesiredNewsRating = updatedAccount.DesiredNewsRating;
                account.AccountStatusId = updatedAccount.AccountStatusId;
                account.AccountStatus = null;
                account.RoleId = updatedAccount.RoleId;
                account.Role = null;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
