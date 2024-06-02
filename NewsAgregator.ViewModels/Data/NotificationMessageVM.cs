using NewsAgregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class NotificationMessageVM
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }

        public Guid? UserId { get; set; }
        public Account? User { get; set; }

        public Guid? AdministratorId { get; set; }
        public Account? Administrator { get; set; }
    }
}
