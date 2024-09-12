using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Mappers.ParametersMappers
{
    [Mapper]
    public static partial class NotificationParametersMapper
    {

        [MapProperty(nameof(Account.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(Account.UserName), nameof(Parameter.Text))]
        public static partial Parameter? AccountToParameter(Account? account);

    }
}
