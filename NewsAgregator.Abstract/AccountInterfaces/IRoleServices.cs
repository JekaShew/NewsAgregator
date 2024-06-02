using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.AccountInterfaces
{
    public interface IRoleServices
    {
        public Task<List<RoleVM>> TakeRoles();

        public Task<RoleVM> TakeRoleById(Guid id);

        public Task AddRole(RoleVM role);

        public Task DeleteRole(Guid id);

        public Task UpdateRole(RoleVM updatedRole);
    }
}
