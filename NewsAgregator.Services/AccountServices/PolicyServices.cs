using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Mappers;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.AccountServices
{
    public class PolicyServices : IPolicyServices
    {
        private readonly AppDBContext _appDBContext;

        public PolicyServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task AddPolicy(PolicyVM policyVM)
        {
            var newPolicy = PolicyMapper.PolicyVMToPolicy(policyVM);
            newPolicy.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newPolicy);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeletePolicy(Guid id)
        {
            _appDBContext.Policies.Remove(await _appDBContext.Policies.FirstOrDefaultAsync(p => p.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<List<PolicyVM>> TakePolicies()
        {
            var policyVMs = (await _appDBContext.Policies.AsNoTracking().ToListAsync()).Select(p => PolicyMapper.PolicyToPolicyVM(p)).ToList();

            return policyVMs;
            
        }

        public async Task<PolicyVM> TakePolicyById(Guid id)
        {
            var policy = PolicyMapper.PolicyToPolicyVM(await _appDBContext.Policies.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id));

            return policy;
        }

        public async Task UpdatePolicy(PolicyVM updatedPolicyVM)
        {
            var policy = await _appDBContext.Policies.FirstOrDefaultAsync(p => p.Id == updatedPolicyVM.Id);

            if (policy != null)
            {
                policy.Title = updatedPolicyVM.Title;
                policy.Description = updatedPolicyVM.Description;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
