using Blog.ViewModels;
using Sakura.AspNetCore;
using System.Collections.Generic;

namespace Blog.Models.Data
{
    public interface IPostService
    {
        
        ICollection<Post> GetAll();
        ICollection<Post> GetAllUnpublished();
        ICollection<Post> FindPostsByText(string text);
        ICollection<Post> GetPostByTag(string tagName);
        Post FindPostById(int id);
        Post FindPostBySlug(string slug);
        void AddPost(Post post);
        void SaveAll();
    }
}