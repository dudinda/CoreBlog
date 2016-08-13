using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Account
{
    public class BlogUser : IdentityUser
    {
        public int Age { set; get; }
        public bool isBanned { get; set; } = false;
    }
}
