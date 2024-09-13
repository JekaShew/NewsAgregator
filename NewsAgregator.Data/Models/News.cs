using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public class News
    {
        [Required]
        public Guid Id { get; set; }
        [Required, MinLength(3)]
        public string? Title { get; set; }
        [Required, MinLength(3)]
        public string? Description { get; set; }
        public string? Text { get; set; }
        public string? TextHTML { get; set; }
        public DateTime? Date { get; set; }
        public float? PositiveRating { get; set; }
        [Required, MinLength(3)]
        public string SourceUrl { get; set; }

        public Guid? SourceId { get; set; }
        public Source? Source { get; set; }

        public Guid? NewsStatusId { get; set; }
        public NewsStatus? NewsStatus { get; set; }

        public List<Complaint>? Complaints { get; set; }

        public List<Comment>? Comments { get; set; }


    }
}
