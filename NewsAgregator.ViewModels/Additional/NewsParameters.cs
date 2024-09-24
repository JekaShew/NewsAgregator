using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Additional
{
    public class NewsParameters
    {
        public List<Parameter>? NewsStatuses { get; set; }
        public List<CommentParameter>? Comments { get; set; } 
    }
}
