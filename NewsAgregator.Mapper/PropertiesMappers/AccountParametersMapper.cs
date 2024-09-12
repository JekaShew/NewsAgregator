using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Mappers.PropertiesMappers
{
    [Mapper]
    public static partial class AccountParametersMapper
    {
        [MapProperty(nameof(AccountStatus.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(AccountStatus.Title), nameof(Parameter.Text))]
        public static partial Parameter? AccountStatusToParameter(AccountStatus? accountStatus);

        [MapProperty(nameof(Role.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(Role.Title), nameof(Parameter.Text))]
        public static partial Parameter? RoleToParameter(Role? role);
    }
}
