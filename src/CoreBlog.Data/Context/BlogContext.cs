using CoreBlog.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoreBlog.Data.Context
{
    public class BlogContext : IdentityDbContext<BlogUser>, IBlogContext
    {
        private IConfigurationRoot config { get; }

        public BlogContext(DbContextOptions<BlogContext> options, IConfigurationRoot config) : base(options)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config["CoreBlogDb:ConnectionString"]);
            base.OnConfiguring(optionsBuilder);
        }



        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }    

    }

}

