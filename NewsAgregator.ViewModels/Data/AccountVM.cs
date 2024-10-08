﻿using NewsAgregator.ViewModels.Additional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class AccountVM
    {
        public Guid Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required, MinLength(3)]
        public string? Login { get; set; }
        [Required]
        public string? FIO { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public float? DesiredNewsRating { get; set; }

        public Guid? AccountStatusId { get; set; }
        public List<Parameter>? AccountStatuses { get; set; }
        public Parameter? AccountStatus { get; set; }


        public Guid? RoleId { get; set; }
        public List<Parameter>? Roles { get; set; }
        public Parameter? Role { get; set; }


        public List<ComplaintVM>? UserComplaints { get; set; }

        public List<ComplaintVM>? AdministratorComplaints { get; set; }

        public List<NotificationMessageVM>? RecipientUsers { get; set; }

        public List<NotificationMessageVM>? SenderAdministrators { get; set; }

        public List<CommentVM>? Comments { get; set; }
    }
}
