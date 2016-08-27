using CoreBlog.Data.Entities;
using System.Collections.Generic;

namespace CoreBlog.Web.Services
{
    public interface IPostService
    {

        /// <summary>
        /// Get published posts
        /// </summary>
        IEnumerable<Post> GetAllPublished();

        /// <summary>
        /// Get unpublished posts
        /// </summary>
        IEnumerable<Post> GetAllUnpublished();

        /// <summary>
        /// Get latest posts
        /// </summary>
        /// <param name="posts">Posts</param>
        /// <param name="count">Posts to take</param>
        IEnumerable<Post> GetLatest(IEnumerable<Post> posts, int count);

        /// <summary>
        /// Get posts by text
        /// </summary>
        IEnumerable<Post> GetPostsByText(string text);


        /// <summary>
        /// Get posts by tag name
        /// </summary>
        IEnumerable<Post> GetPostsByTag(string tagName);

        /// <summary>
        /// Get posts by category
        /// </summary>
        /// <param name="categoryName">Category name</param>
        IEnumerable<Post> GetPostsByCategory(string categoryName);


        /// <summary>
        /// Get a counter 
        /// </summary>
        /// <param name="categoryName">Category name</param>
        int GetCategoryCounter(IEnumerable<Post> collection, string categoryName);

        /// <summary>
        /// Get single post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Post object</returns>
        Post GetPostById(int id);

        /// <summary>
        /// Add new post
        /// </summary>
        /// <param name="post">Post</param>
        void AddPost(Post post);

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="post">Post</param>
        void UpdatePost(Post post);

        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="id">Post</param>
        void RemovePost(Post post);

        /// <summary>
        /// Save all
        /// </summary>
        bool SaveAll();
        
    }
}