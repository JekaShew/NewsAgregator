using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Text { get; set; }
        public DateTime? Date { get; set; }
        public float? PositiveRating { get; set; }
        public string SourceUrl { get; set; }

        public Guid? SourceId { get; set; }
        public Source? Source { get; set; }

        public Guid? NewsStatusId { get; set; }
        public NewsStatus? NewsStatus { get; set; }

        public List<Complaint>? Complaints { get; set; }

        public List<Comment>? Comments { get; set; }


    }
}
