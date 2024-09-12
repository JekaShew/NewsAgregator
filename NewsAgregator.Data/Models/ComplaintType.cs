using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class ComplaintType
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }

        public List<Complaint>? Complaints { get; set; }

    }
}
