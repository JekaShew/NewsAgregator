using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Mappers
{
    [Mapper]
    public static partial class NewsStatusMapper
    {
        public static partial NewsStatusVM? NewsStatusToNewsStatusVM(NewsStatus? newsStatus);

        public static partial NewsStatus? NewsStatusVMToNewsStatus(NewsStatusVM? newsStatusVM);

    }
}
