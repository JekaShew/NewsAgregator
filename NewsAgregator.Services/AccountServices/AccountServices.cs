using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
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

        public async Task AddAccount(AccountVM account)
        {
            var newAccount = _mapper.Map<Data.Models.Account>(account);
            newAccount.Id = Guid.NewGuid();
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

            return account;
        }

        public async Task<List<AccountVM>> TakeAccounts()
        {
            var accountVMs = _mapper.Map<List<AccountVM>>(await _appDBContext.Accounts
                .AsNoTracking()
                .Include(acc => acc.AccountStatus)
                .Include(acc => acc.Role)
                .ToListAsync());

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
