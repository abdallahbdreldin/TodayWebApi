﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayWebAPi.DAL.Data.Identity
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
