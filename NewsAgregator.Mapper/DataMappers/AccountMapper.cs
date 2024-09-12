using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Mapper.DataMappers
{
    [Mapper]
    public static partial class AccountMapper
    {
        [MapProperty([nameof(Account.AccountStatus), nameof(Account.AccountStatus.Id)],
            [nameof(AccountVM.AccountStatus), nameof(AccountVM.AccountStatus.Id)])]
        [MapProperty([nameof(Account.AccountStatus), nameof(Account.AccountStatus.Title)],
            [nameof(AccountVM.AccountStatus), nameof(AccountVM.AccountStatus.Text)])]

        [MapProperty([nameof(Account.Role), nameof(Account.Role.Id)],
            [nameof(AccountVM.Role), nameof(AccountVM.Role.Id)])]
        [MapProperty([nameof(Account.Role), nameof(Account.Role.Title)],
            [nameof(AccountVM.Role), nameof(AccountVM.Role.Text)])]
        public static partial AccountVM? AccountToAccountVM(Account? account);

        public static partial Account? AccountVMToAccount(AccountVM? accountVM);

    }
}
