using AutoMapper;
using Mapper.Mappers.PropertiesMappers;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Mappers;
using NewsAgregator.Data.Models;
using NewsAgregator.Mapper.DataMappers;
using NewsAgregator.ViewModels.Additional;
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

        public RoleServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task AddRoleAsync(RoleVM role)
        {
            var newRole = RoleMapper.RoleVMToRole(role);
            newRole.Id = Guid.NewGuid();

            _appDBContext.RolePolicies.AddRange(role.RolePolicies.Select(rp => RolePolicyMapper.RolePolicyVMToRolePolicy(rp)));

            await _appDBContext.AddAsync(newRole);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            _appDBContext.Roles.Remove(await _appDBContext.Roles.FirstOrDefaultAsync(r => r.Id == id));
            _appDBContext.RolePolicies.RemoveRange(await _appDBContext.RolePolicies.Where(rp => rp.RoleId == id).ToListAsync());
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<RoleVM> TakeRoleByIdAsync(Guid id)
        {
            var roleVM = RoleMapper.RoleToRoleVM(await _appDBContext.Roles
                .AsNoTracking()
                .Include(rp => rp.RolePolicies)
                    .ThenInclude(p => p.Policy)
                .FirstOrDefaultAsync(r => r.Id == id));
            roleVM.RolePolicies = (await _appDBContext.RolePolicies.Where(rp=> rp.RoleId == id).ToListAsync()).Select(rp=> RolePolicyMapper.RolePolicyToRolePolicyVM(rp)).ToList();

            return roleVM;
        }

        public async Task<List<RoleVM>> TakeRolesAsync()
        {
            var roleVMs = (await _appDBContext.Roles
                .AsNoTracking()
                .Include(rp => rp.RolePolicies)
                    .ThenInclude(p => p.Policy)
                .ToListAsync()).Select(r => RoleMapper.RoleToRoleVM(r)).ToList();
            foreach(var roleVM in roleVMs)
            {
                roleVM.RolePolicies = (await _appDBContext.RolePolicies.Where(rp => rp.RoleId == roleVM.Id).ToListAsync()).Select(rp => RolePolicyMapper.RolePolicyToRolePolicyVM(rp)).ToList();
            }
            return roleVMs;
        }

        public async Task UpdateRoleAsync(RoleVM updatedRoleVM)
        {
            var role = await _appDBContext.Roles.Include(rp => rp.RolePolicies).FirstOrDefaultAsync(r => r.Id == updatedRoleVM.Id);

            if (role != null)
            {
                role.Title = updatedRoleVM.Title;
                role.Description = updatedRoleVM.Description;

                _appDBContext.RolePolicies.RemoveRange(role.RolePolicies);
                await _appDBContext.RolePolicies.AddRangeAsync(updatedRoleVM.RolePolicies.Select(urp => RolePolicyMapper.RolePolicyVMToRolePolicy(urp)).ToList());

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
