using NewsAgregator.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class NewsStatusVM
    {
        public Guid Id { get; set; }
        [Required, MinLength(3)]
        public string? Title { get; set; }
        public string? Description { get; set; }

        public List<NewsVM>? Newses { get; set; }
    }
}
