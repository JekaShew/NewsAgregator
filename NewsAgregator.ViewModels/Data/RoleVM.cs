using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class RoleVM
    {
        public Guid Id { get; set; }
        [Required, MinLength(3)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<Parameter>? Policies { get; set; }
        public List<Guid>? PoliciesIDs { get; set; }

        public List<RolePolicyVM>? RolePolicies { get; set; }
        public List<CreateAccountVM>? Accounts { get; set; }
    }
}
