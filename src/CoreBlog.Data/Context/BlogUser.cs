using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CoreBlog.Data.Context
{
    public class BlogUser : IdentityUser
    {
        public int Age { set; get; }
        public bool isBanned { get; set; } = false;
    }
}
