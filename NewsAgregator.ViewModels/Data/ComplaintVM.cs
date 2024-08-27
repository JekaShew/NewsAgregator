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
        [Required]
        public Guid Id { get; set; }
        [Required, MinLength(1)]
        public string? Title { get; set; }
        [Required,MinLength(5)]
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

        public void FromDataModel(Comment comment, News news,ComplaintStatus complaintStatus,ComplaintType complaintType,Account user,Account administrator)
        {
            Comment = new Parameter
            {
                Id = comment != null ? comment.Id : null,
                Text = comment != null ? comment.Text : "",

            };

            News = new Parameter
            {
                Id = news != null ? news.Id : null,
                Text = news != null ? news.Title : "",
            };

            ComplaintStatus = new Parameter
            {
                Id = complaintStatus != null ? complaintStatus.Id : null,
                Text = complaintStatus != null ? complaintStatus.Title : "",
            };

            ComplaintType = new Parameter
            {
                Id = complaintType != null ? complaintType.Id : null,
                Text = complaintType != null ? complaintType.Title : "",
            };

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
