using Blog.Models.Account;
using Blog.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.PostViewModels
{
    public class BlogContext : IdentityDbContext<BlogUser>

    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options) {
           
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>()
                .HasMany(option => option.Tags)
                .WithOne();
                   
        }

    }

}

