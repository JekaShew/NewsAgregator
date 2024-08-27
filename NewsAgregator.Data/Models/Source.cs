using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Models
{
    public  class Source
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }   
        public string? BaseUrl { get; set; } 
        public string? RssUrl { get; set; }
        public string? Url { get; set; }

        public List<News>? News { get; set; }


    }
}
