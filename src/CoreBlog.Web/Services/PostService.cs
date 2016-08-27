using CoreBlog.Data.Context;
using CoreBlog.Data.Entities;
using CoreBlog.Data.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CoreBlog.Web.Services
{
    public class PostService : IPostService
    {
        private IBlogContext context { get; }

        public PostService(IBlogContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add new post
        /// </summary>
        /// <param name="post">Post</param>
        public void AddPost(Post post)
        {
            context.Posts.Add(post);
        }

        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="id">Post</param>
        public void RemovePost(Post post)
        {
            context.Posts.Remove(post);
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="post">Post</param>
        public void UpdatePost(Post post)
        {
            context.Posts.Update(post);
        }

        /// <summary>
        /// Save all
        /// </summary>
        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }

        /// <summary>
        /// Get unpublished posts
        /// </summary>
        public IEnumerable<Post> GetAllUnpublished()
        {
            return context
                        .Posts
                        .Where(post => !post.IsPublished)
                        .Include(post => post.Tags)
                        .Include(post => post.Image)
                        .Include(post => post.Category);
        }

        /// <summary>
        /// Get published posts
        /// </summary>
        public IEnumerable<Post> GetAllPublished()
        {
            return context
                        .Posts
                        .Where(post => post.IsPublished)
                        .Include(post => post.Tags)
                        .Include(post => post.Image)
                        .Include(post => post.Category);
        }


        /// <summary>
        /// Get latest posts
        /// </summary>
        /// <param name="posts">Posts</param>
        /// <param name="count">Posts to take</param>
        public IEnumerable<Post> GetLatest(IEnumerable<Post> posts, int count)
        {
            return posts
                        .TakeLast(count)
                        .OrderByDescending(post => post.PostedOn);
        }
            

        /// <summary>
        /// Get single post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Post object</returns>
        public Post GetPostById(int id)
        {        
            return context
                        .Posts
                        .Where(option => option.Id == id)?
                        .Include(post => post.Tags)
                        .Include(post => post.Image)
                        .Include(post => post.Category)
                        .Single(); 
        }


        /// <summary>
        /// Get posts by category
        /// </summary>
        /// <param name="categoryName">Category name</param>
        public IEnumerable<Post> GetPostsByCategory(string categoryName)
        {           
            return GetAllPublished()
                        .Where(category => category.Category.Name.Equals(categoryName));                
        }

        /// <summary>
        /// Get a counter 
        /// </summary>
        /// <param name="categoryName">Category name</param>
        public int GetCategoryCounter(IEnumerable<Post> collection, string categoryName)
        {
            return collection
                        .Where(post => post.Category.Name == categoryName)
                        .Count();
        }


        /// <summary>
        /// Get posts by text
        /// </summary>
        public IEnumerable<Post> GetPostsByText(string text)
        {
            //search in title/short description/description

            return GetAllPublished()
                        .Where(option => option.Description.Contains(text, StringComparison.OrdinalIgnoreCase)       ||
                                         option.ShortDescription.Contains(text, StringComparison.OrdinalIgnoreCase)  ||
                                         option.Title.Contains(text, StringComparison.OrdinalIgnoreCase)
                                       );           
        }


        /// <summary>
        /// Get posts by tag name
        /// </summary>
        public IEnumerable<Post> GetPostsByTag(string tagName)
        {
            //get all posts which contain tagName
            return GetAllPublished()
                        .Where(option => option.Tags.Any(name => name.Name == tagName));
            
        }
    }
}
