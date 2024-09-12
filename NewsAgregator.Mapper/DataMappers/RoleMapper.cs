using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Mappers
{
    [Mapper]
    public static partial class RoleMapper
    {

        public static partial RoleVM? RoleToRoleVM(Role? role);

        public static partial Role? RoleVMToRole(RoleVM? roleVM);

    }
}
