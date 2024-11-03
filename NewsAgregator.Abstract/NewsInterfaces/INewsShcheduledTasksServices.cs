using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.NewsInterfaces
{
    public interface INewsShcheduledTasksServices
    {
        public Task DeleteNewsWithBadRateAsync();
        public Task DeleteOldNewsesAsync();
    }
}
