using NewsAgregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class RolePolicyVM
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }

        public Guid? RoleId { get; set; }
        public RoleVM? Role { get; set; }

        public Guid? PolicyId { get; set; }
        public PolicyVM? Policy { get; set; }
    }
}
