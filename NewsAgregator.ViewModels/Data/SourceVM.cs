using NewsAgregator.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class SourceVM
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        [Required]
        public string? BaseUrl { get; set; }
        public string? RssUrl { get; set; }

        public List<News>? News { get; set; }


    }
}
