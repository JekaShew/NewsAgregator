using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.AccountServices
{
    public class RoleServices : IRoleServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;

        public RoleServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }

        public async Task AddRole(RoleVM role)
        {
            var newRole = _mapper.Map<Data.Models.Role>(role);
            newRole.Id = Guid.NewGuid();

            _appDBContext.RolePolicies.AddRange(role.RolePolicies.Select(rp => new Data.Models.RolePolicy
            {
                Id = rp.Id,
                RoleId = newRole.Id,
                PolicyId = rp.Policy.Id,
                Description = rp.Description
            }));

            await _appDBContext.AddAsync(newRole);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteRole(Guid id)
        {
            _appDBContext.Roles.Remove(await _appDBContext.Roles.FirstOrDefaultAsync(r => r.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<RoleVM> TakeRoleById(Guid id)
        {
            var role = _mapper.Map<RoleVM>(await _appDBContext.Roles
                .AsNoTracking()
                .Include(rp => rp.RolePolicies)
                    .ThenInclude(p => p.Policy)
                .FirstOrDefaultAsync(r => r.Id == id));

            return role;
        }

        public async Task<List<RoleVM>> TakeRoles()
        {
            var roleVMs = _mapper.Map<List<RoleVM>>(await _appDBContext.Roles
                .AsNoTracking()
                .Include(rp => rp.RolePolicies)
                    .ThenInclude(p => p.Policy)
                .ToListAsync());

            return roleVMs;
        }

        public async Task UpdateRole(RoleVM updatedRole)
        {
            var role = await _appDBContext.Roles.Include(rp => rp.RolePolicies).FirstOrDefaultAsync(r => r.Id == updatedRole.Id);

            if (role != null)
            {
                role.Title = updatedRole.Title;
                role.Description = updatedRole.Description;

                _appDBContext.RolePolicies.RemoveRange(role.RolePolicies);
                var rolePolicies = new List<Data.Models.RolePolicy>();
                foreach (var rolePolicy in updatedRole.RolePolicies)
                {

                    role.RolePolicies.Add(_mapper.Map<Data.Models.RolePolicy>(rolePolicy));
                }

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
