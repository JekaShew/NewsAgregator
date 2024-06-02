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
        public Task<List<PolicyVM>> TakePolicies();

        public Task<PolicyVM> TakePolicyById(Guid id);

        public Task AddPolicy(PolicyVM policy);

        public Task DeletePolicy(Guid id);

        public Task UpdatePolicy(PolicyVM updatedPolicy);
    }
}
