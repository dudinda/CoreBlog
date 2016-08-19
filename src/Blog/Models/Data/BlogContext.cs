using Blog.Models.Account;
using Blog.Models.Data;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Blog.Models.PostViewModels
{
    public class BlogContext : IdentityDbContext<BlogUser>

    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options) {
           
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
      

    }

}

