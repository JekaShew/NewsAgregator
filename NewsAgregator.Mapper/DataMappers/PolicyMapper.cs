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
    public static partial class PolicyMapper
    {
        public static partial PolicyVM? PolicyToPolicyVM(Policy? policy);

        public static partial Policy? PolicyVMToPolicy(PolicyVM? policyVM);

    }
}
