using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class ComplaintVM
    {
        public Guid Id { get; set; }
        [Required, MinLength(1)]
        public string? Title { get; set; }
        [Required, MinLength(5)]
        public string? Text { get; set; }

        public Guid? CommentId { get; set; }
        public List<Parameter>? Comments { get; set; }
        public Parameter? Comment { get; set; }

        public Guid? NewsId { get; set; }
        public List<Parameter>? Newses { get; set; }
        public Parameter? News { get; set; }

        public Guid? ComplaintStatusId { get; set; }
        public List<Parameter>? ComplaintStatuses { get; set; }
        public Parameter? ComplaintStatus { get; set; }

        public Guid? ComplaintTypeId { get; set; }
        public List<Parameter>? ComplaintTypes { get; set; }
        public Parameter? ComplaintType { get; set; }

        public List<Parameter>? Accounts { get; set; }
        public Guid? UserId { get; set; }
        public Parameter? User { get; set; }

        public Guid? AdministratorId { get; set; }
        public Parameter? Administrator { get; set; }

    }
}