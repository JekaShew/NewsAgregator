using NewsAgregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class NewsVM
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime? Date { get; set; }
        public float? PositiveRating { get; set; }
        public string? Source { get; set; }

        public Guid? NewsStatusId { get; set; }
        public NewsStatusVM? NewsStatus { get; set; }

        public List<ComplaintVM>? Complaints { get; set; }

        public List<CommentVM>? Comments { get; set; }
    }
}
