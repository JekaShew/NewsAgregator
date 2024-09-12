using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class NotificationMessage
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Text { get; set; }

        public Guid? UserId { get; set; }
        public Account? User { get; set; }

        public Guid? AdministratorId { get; set; }
        public Account? Administrator { get; set; }
    }
}
