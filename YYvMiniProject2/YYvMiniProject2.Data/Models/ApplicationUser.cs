﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYvMiniProject2.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public int bookToken { get; set; }
    }
}
