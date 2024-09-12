using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class RolePolicy
    {
        [Required]
        public Guid Id { get; set; }
        public string? Description { get; set; }

        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }

        public Guid? PolicyId { get; set; }
        public Policy? Policy { get; set; }
    }
}
