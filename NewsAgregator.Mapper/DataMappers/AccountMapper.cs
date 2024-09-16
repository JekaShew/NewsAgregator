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
            [nameof(CreateAccountVM.AccountStatus), nameof(CreateAccountVM.AccountStatus.Id)])]
        [MapProperty([nameof(Account.AccountStatus), nameof(Account.AccountStatus.Title)],
            [nameof(CreateAccountVM.AccountStatus), nameof(CreateAccountVM.AccountStatus.Text)])]

        [MapProperty([nameof(Account.Role), nameof(Account.Role.Id)],
            [nameof(CreateAccountVM.Role), nameof(CreateAccountVM.Role.Id)])]
        [MapProperty([nameof(Account.Role), nameof(Account.Role.Title)],
            [nameof(CreateAccountVM.Role), nameof(CreateAccountVM.Role.Text)])]
        public static partial AccountVM? AccountToAccountVM(Account? account);

        //public static partial CreateAccountVM? AccountToAccountVM(Account? account);

        public static partial Account? CreateAccountVMToAccount(CreateAccountVM? createAccountVM);

    }
}
