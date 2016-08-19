using Blog.Models.Data;
using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models.PostViewModels
{
    public interface IBlogContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Image> Images { get; set; }
    }
}