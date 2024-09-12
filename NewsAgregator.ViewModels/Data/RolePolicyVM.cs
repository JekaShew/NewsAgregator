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
    public class RolePolicyVM
    {
        [Required]
        public Guid Id { get; set; }
        public string? Description { get; set; }

        public Guid? RoleId { get; set; }
        public Parameter? Role { get; set; }

        public Guid? PolicyId { get; set; }
        public Parameter? Policy { get; set; }
    }
}
