using CoreBlog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreBlog.Data.Context
{
    public interface IBlogContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Image> Images { get; set; }

        int SaveChanges();

    }
}