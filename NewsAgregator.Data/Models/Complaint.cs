using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class Complaint
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Text { get; set; }

        public Guid? CommentId { get; set; }
        public Comment? Comment { get; set; }

        public Guid? NewsId { get; set; }
        public News? News { get; set; }

        public Guid? ComplaintStatusId { get; set; }
        public ComplaintStatus? ComplaintStatus { get; set; }

        public Guid? ComplaintTypeId { get; set; }
        public ComplaintType? ComplaintType { get; set; }

        public Guid? UserId { get; set; }
        public Account? User { get; set; }

        public Guid? AdministratorId { get; set; }
        public Account? Administrator { get; set; }
    }
}
