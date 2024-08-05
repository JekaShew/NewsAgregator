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
    public class NotificationMessageVM
    {
        [Required]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }

        public List<Parameter>? Accounts { get; set; }
        public Guid? UserId { get; set; }
        public Parameter? User { get; set; }

        public Guid? AdministratorId { get; set; }
        public Parameter? Administrator { get; set; }

        public void FromDataModel( Account user, Account administrator)
        {
            User = new Parameter
            {
                Id = user != null ? user.Id : null,
                Text = user != null ? user.UserName : "",

            };

            Administrator = new Parameter
            {
                Id = administrator != null ? administrator.Id : null,
                Text = administrator != null ? administrator.UserName : "",
            };

        }
    }
}
