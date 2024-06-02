using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public DateTime? Date { get; set; }

        public Guid? AccountId { get; set; }
        public Account? Account { get; set; }

        public Guid? NewsId { get; set; }
        public News? News { get; set; }

        public List<Complaint>? Complaints { get; set; } 
        
    }
}
