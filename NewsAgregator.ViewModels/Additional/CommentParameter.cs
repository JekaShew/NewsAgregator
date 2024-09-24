using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Additional
{
    public class CommentParameter
    {
        public Guid Id { get; set; }
        public Guid? NewsId { get; set; } 
        public string? UserName { get; set; }
        public DateTime? Date { get; set; }
        public string? Text { get; set; }
    }
}
