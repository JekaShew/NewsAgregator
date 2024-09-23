using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
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
    public static partial class RolePolicyMapper
    {
        [MapProperty([nameof(RolePolicy.Role), nameof(RolePolicy.Role.Id)],
            [nameof(RolePolicyVM.Role), nameof(RolePolicyVM.Role.Id)])]
        [MapProperty([nameof(RolePolicy.Role), nameof(RolePolicy.Role.Title)],
            [nameof(RolePolicyVM.Role), nameof(RolePolicyVM.Role.Text)])]

        [MapProperty([nameof(RolePolicy.Policy), nameof(RolePolicy.Policy.Id)],
            [nameof(RolePolicyVM.Policy), nameof(RolePolicyVM.Policy.Id)])]
        [MapProperty([nameof(RolePolicy.Policy), nameof(RolePolicy.Policy.Title)],
            [nameof(RolePolicyVM.Policy), nameof(RolePolicyVM.Policy.Text)])]
        public static partial RolePolicyVM? RolePolicyToRolePolicyVM(RolePolicy? rolePolicy);

        public static partial RolePolicy? RolePolicyVMToRolePolicy(RolePolicyVM? rolePolicyVM);

        [MapProperty(nameof(Parameter.Id), nameof(RolePolicy.PolicyId))]
        public static partial RolePolicy? PolicyParameterToRolePolicy(Parameter? policyParameter);

    }
}
