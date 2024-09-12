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
        public Task<List<RoleVM>> TakeRolesAsync();

        public Task<RoleVM> TakeRoleByIdAsync(Guid id);

        public Task AddRoleAsync(RoleVM role);

        public Task DeleteRoleAsync(Guid id);

        public Task UpdateRoleAsync(RoleVM updatedRole);
    }
}
