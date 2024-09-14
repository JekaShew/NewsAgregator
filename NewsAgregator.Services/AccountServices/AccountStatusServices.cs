using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Mappers;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.AccountServices
{
    public class AccountStatusServices : IAccountStatusServices
    {
        private readonly AppDBContext _appDBContext;
        public AccountStatusServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task AddAccountStatusAsync(AccountStatusVM accountStatusVM)
        {
            var newAccountStatus = AccountStatusMapper.AccountStatusVMToAccountStatus(accountStatusVM);
            newAccountStatus.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newAccountStatus);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteAccountStatusAsync(Guid id)
        {
            _appDBContext.AccountStatuses.Remove(await _appDBContext.AccountStatuses.FirstOrDefaultAsync(accs => accs.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<AccountStatusVM> TakeAccountStatusByIdAsync(Guid id)
        {
            var accountStatus = AccountStatusMapper.AccountStatusToAccountStatusVM(await _appDBContext.AccountStatuses.AsNoTracking().FirstOrDefaultAsync(accs => accs.Id == id));

            return accountStatus;
        }

        public async Task<List<AccountStatusVM>> TakeAccountStatusesAsync()
        {
            var accountStatusVMs = (await _appDBContext.AccountStatuses.AsNoTracking().ToListAsync()).Select(ac => AccountStatusMapper.AccountStatusToAccountStatusVM(ac)).ToList();

            return accountStatusVMs;
        }

        public async Task UpdateAccountStatusAsync(AccountStatusVM updatedAccountStatusVM)
        {
            var accountStatus = await _appDBContext.AccountStatuses.FirstOrDefaultAsync(accs => accs.Id == updatedAccountStatusVM.Id);

            if (accountStatus != null)
            {
                accountStatus.Title = updatedAccountStatusVM.Title;
                accountStatus.Description = updatedAccountStatusVM.Description;

                await _appDBContext.SaveChangesAsync();
            }
            else return;  
        }
    }
}
