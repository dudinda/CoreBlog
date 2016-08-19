using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Blog.Models.Account
{
    public class BlogUser : IdentityUser
    {
        public int Age { set; get; }
        public bool isBanned { get; set; } = false;
    }
}
