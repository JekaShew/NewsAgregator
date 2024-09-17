using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
     public class RefreshToken
    {
        public Guid Id { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public List<Account>? Accounts { get; set; }
    }
}
