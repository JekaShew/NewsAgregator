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
    public static partial class SourceMapper
    {
        public static partial SourceVM? SourceToSourceVM(Source? source);

        public static partial Source? SourceVMToSource(SourceVM? sourceVM);

    }
}
