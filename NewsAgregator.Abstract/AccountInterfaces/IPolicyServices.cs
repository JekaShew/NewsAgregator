using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.AccountInterfaces
{
    public interface IPolicyServices
    {
        public Task<List<PolicyVM>> TakePoliciesAsync();

        public Task<PolicyVM> TakePolicyByIdAsync(Guid id);

        public Task AddPolicyAsync(PolicyVM policy);

        public Task DeletePolicyAsync(Guid id);

        public Task UpdatePolicyAsync(PolicyVM updatedPolicy);
    }
}
