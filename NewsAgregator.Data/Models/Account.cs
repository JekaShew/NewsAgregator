using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? FIO { get; set; }
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

        public List<NotificationMessage>? SenderAdministrators {  get; set; }    

        public List<Comment>? Comments { get; set; }


    }
}
