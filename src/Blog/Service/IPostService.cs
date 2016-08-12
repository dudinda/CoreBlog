using Blog.ViewModels;
using Sakura.AspNetCore;
using System.Collections.Generic;

namespace Blog.Models.Data
{
    public interface IPostService
    {
        
        ICollection<Post> GetAll();
        ICollection<Post> GetLatest(ref ICollection<Post> posts, int count);
        ICollection<Post> GetAllUnpublished();

        ICollection<Post> GetPostsByText(string text);
        ICollection<Post> GetPostsByTag(string tagName);
        ICollection<Post> GetPostsByCategory(string categoryName);

        Post GetPostById(int id);
        Post GetPostBySlug(string slug);

        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(Post post);

        bool SaveAll();
        
    }
}