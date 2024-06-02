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
    public class PolicyServices : IPolicyServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;

        public PolicyServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }

        public async Task AddPolicy(PolicyVM policy)
        {
            var newPolicy = _mapper.Map<Data.Models.Policy>(policy);
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
            var policyVMs = _mapper.Map<List<PolicyVM>>(await _appDBContext.Policies.AsNoTracking().ToListAsync());

            return policyVMs;
            
        }

        public async Task<PolicyVM> TakePolicyById(Guid id)
        {
            var policy = _mapper.Map<PolicyVM>(await _appDBContext.Policies.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id));

            return policy;
        }

        public async Task UpdatePolicy(PolicyVM updatedPolicy)
        {
            var policy = await _appDBContext.Policies.FirstOrDefaultAsync(p => p.Id == updatedPolicy.Id);

            if (policy != null)
            {
                policy.Title = updatedPolicy.Title;
                policy.Description = updatedPolicy.Description;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
