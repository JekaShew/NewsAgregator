using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
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
        private readonly IMapper _mapper;
        public AccountStatusServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }
        public async Task AddAccountStatus(AccountStatusVM accountStatus)
        {
            var newAccountStatus = _mapper.Map<Data.Models.AccountStatus>(accountStatus);
            newAccountStatus.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newAccountStatus);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteAccountStatus(Guid id)
        {
            _appDBContext.AccountStatuses.Remove(await _appDBContext.AccountStatuses.FirstOrDefaultAsync(accs => accs.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<AccountStatusVM> TakeAccountStatusById(Guid id)
        {
            var accountStatus = _mapper.Map<AccountStatusVM>(await _appDBContext.AccountStatuses.AsNoTracking().FirstOrDefaultAsync(accs => accs.Id == id));

            return accountStatus;
        }

        public async Task<List<AccountStatusVM>> TakeAccountStatuses()
        {
            var accountStatusVMs = _mapper.Map<List<AccountStatusVM>>(await _appDBContext.AccountStatuses.AsNoTracking().ToListAsync());

            return accountStatusVMs;
        }

        public async Task UpdateAccountStatus(AccountStatusVM updatedAccountStatus)
        {
            var accountStatus = await _appDBContext.AccountStatuses.FirstOrDefaultAsync(accs => accs.Id == updatedAccountStatus.Id);

            if (accountStatus != null)
            {
                accountStatus.Title = updatedAccountStatus.Title;
                accountStatus.Description = updatedAccountStatus.Description;

                await _appDBContext.SaveChangesAsync();
            }
            else return;  
        }
    }
}
