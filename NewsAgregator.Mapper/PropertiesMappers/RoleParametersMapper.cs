using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Mapper.PropertiesMappers
{
    [Mapper]
    public static partial class RoleParametersMapper
    {
        [MapProperty(nameof(Policy.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(Policy.Title), nameof(Parameter.Text))]
        public static partial Parameter? PolicyToParameter(Policy? policy);

    }
}
