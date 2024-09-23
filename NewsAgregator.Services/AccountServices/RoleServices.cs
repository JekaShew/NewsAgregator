using AutoMapper;
using Mapper.Mappers.PropertiesMappers;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Mappers;
using NewsAgregator.Data.Models;
using NewsAgregator.Mapper.DataMappers;
using NewsAgregator.Mapper.PropertiesMappers;
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

        public async Task<RoleParameters> GetRoleParametersAsync()
        {
            //var test = await _appDBContext.RolePolicies.AsNoTracking().Include(p => p.Policy).ToListAsync();
            var roleParameters = new RoleParameters()
            {
                Policies = (await _appDBContext.Policies.AsNoTracking().ToListAsync()).Select(p => RoleParametersMapper.PolicyToParameter(p)).ToList(),
            };
            return roleParameters;

        }

        public async Task AddRoleAsync(RoleVM roleVM)
        {
            var newRole = RoleMapper.RoleVMToRole(roleVM);
            newRole.Id = Guid.NewGuid();

            _appDBContext.RolePolicies.AddRange(roleVM.PoliciesIDs.Where(p => p != Guid.Empty).Select(p => 
                                        new RolePolicy 
                                        { 
                                            Id = Guid.NewGuid(),
                                            RoleId = newRole.Id,
                                            PolicyId = p
                                        }));

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
            var roleParameters = await GetRoleParametersAsync();
            roleVM.Policies = roleParameters.Policies;

            return roleVM;
        }

        public async Task<List<RoleVM>> TakeRolesAsync()
        {
            var roleVMs = (await _appDBContext.Roles
                .AsNoTracking()
                .Include(rp => rp.RolePolicies)
                    .ThenInclude(p => p.Policy)
                .ToListAsync()).Select(r => RoleMapper.RoleToRoleVM(r)).ToList();
            var policies = (await GetRoleParametersAsync()).Policies;
            foreach(var roleVM in roleVMs)
            {
               var policyIds = await _appDBContext.RolePolicies.Where(rp => rp.RoleId == roleVM.Id).Select(rp => rp.PolicyId).ToListAsync();
                roleVM.Policies = policies.Where(p =>  policyIds.Contains(p.Id)).ToList();
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
                _appDBContext.RolePolicies.AddRange(updatedRoleVM.Policies.Select(p =>
                                        new RolePolicy
                                        {
                                            Id = Guid.NewGuid(),
                                            RoleId = updatedRoleVM.Id,
                                            PolicyId = p.Id
                                        }));
                //await _appDBContext.RolePolicies.AddRangeAsync(updatedRoleVM.RolePolicies.Select(urp => RolePolicyMapper.RolePolicyVMToRolePolicy(urp)).ToList());

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
