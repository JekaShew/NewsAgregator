using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class Account
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required, MinLength(3)]
        public string? Login { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
        [Required]
        public string? SecurityStamp { get; set; }
        [Required, MinLength(6)]
        public string? SecretWord { get; set; }
        [Required]
        public string? FIO { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public float? DesiredNewsRating { get; set; }

        public Guid? AccountStatusId { get; set; }
        public AccountStatus? AccountStatus { get; set; }

        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }

        public List<Complaint>? UserComplaints { get; set; }

        public List<Complaint>? AdministratorComplaints { get; set; }

        public List<NotificationMessage>? RecipientUsers { get; set; }

        public List<NotificationMessage>? SenderAdministrators { get; set; }

        public List<Comment>? Comments { get; set; }


    }
}
