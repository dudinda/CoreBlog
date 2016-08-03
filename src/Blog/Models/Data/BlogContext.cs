using Blog.Models.Account;
using Blog.Models.Data;
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
        public DbSet<UniqueTag> UniqueTags { get; set; }
        public DbSet<Category> Categories { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            base.OnModelCreating(modelBuilder);
        

        }

    }

}

