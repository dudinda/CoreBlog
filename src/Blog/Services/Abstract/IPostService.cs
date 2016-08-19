using System.Collections.Generic;

namespace Blog.Models.Data
{
    public interface IPostService
    {

        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetLatest(IEnumerable<Post> posts, int count);
        IEnumerable<Post> GetAllUnpublished();

        IEnumerable<Post> GetPostsByText(string text);
        IEnumerable<Post> GetPostsByTag(string tagName);
        IEnumerable<Post> GetPostsByCategory(string categoryName);

        int GetCategoryCounter(IEnumerable<Post> collection, string categoryName);

        Post GetPostById(int id);
        Post GetPostBySlug(string slug);

        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(Post post);

        bool SaveAll();
        
    }
}