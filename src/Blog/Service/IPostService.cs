using System.Collections.Generic;

namespace Blog.Models.Data
{
    public interface IPostService
    {
        
        ICollection<Post> GetAll();
        ICollection<Post> FindPostsByText(string text);
        ICollection<Post> GetPostByTag(string tagName);
        Post FindPostById(int id);
        Post FindPostBySlug(string slug);
        Pager GetPagedPosts(ICollection<Post> posts, int page = 1, int pageSize = 5);
        void AddPost(Post post);
        void SaveAll();
    }
}