﻿using NewsAgregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class RoleVM
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public List<RolePolicyVM>? RolePolicies { get; set; }
        public List<AccountVM>? Accounts { get; set; }
    }
}
