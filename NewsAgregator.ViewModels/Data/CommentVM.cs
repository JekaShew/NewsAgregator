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
    public class CommentVM
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public DateTime? Date { get; set; }

        public Guid? AccountId { get; set; }
        public List<Parameter>? Accounts { get; set; }
        public Parameter? Account { get; set; }

        public Guid? NewsId { get; set; }
        public List<Parameter>? Newses { get; set; }
        public Parameter? News { get; set; }

        public List<Complaint>? Complaints { get; set; }

        //public void FromDataModel(Account account, News news)
        //{
        //    Account = new Parameter
        //    {
        //        Id = account != null ? account.Id : null,
        //        Text = account != null ? account.UserName : "",

        //    };

        //    News = new Parameter
        //    {
        //        Id = news != null ? news.Id : null,
        //        Text = news != null ? news.Title : "",
        //    };

        //}
    }
}
