using CoreBlog.Data.Context;
using CoreBlog.Data.Entities;
using CoreBlog.Data.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoreBlog.Web.Services
{
    public class PostService : IPostService
    {
        private IBlogContext context { get; }

        public PostService(IBlogContext context)
        {
            this.context = context;
        }

        public void AddPost(Post post)
        {
            context.Posts.Add(post);
        }

        public void RemovePost(Post post)
        {
            context.Posts.Remove(post);
        }

        public void UpdatePost(Post post)
        {
            context.Posts.Update(post);
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }

        public IEnumerable<Post> GetAllUnpublished()
        {
            return context
                        .Posts
                        .Where(post => !post.IsPublished)
                        .Include(post => post.Tags)
                        .Include(post => post.Image)
                        .Include(post => post.Category);
        }

        public IEnumerable<Post> GetAll()
        {
            return context
                        .Posts
                        .Where(post => post.IsPublished)
                        .Include(post => post.Tags)
                        .Include(post => post.Image)
                        .Include(post => post.Category);
        }

        public IEnumerable<Post> GetLatest(IEnumerable<Post> posts, int count)
        {
            return posts
                        .TakeLast(5)
                        .OrderByDescending(post => post.PostedOn);
        }
            

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

        public Post GetPostBySlug(string slug)
        {
            return context
                        .Posts
                        .Where(option => option.UrlSlug == slug)
                        .Include(post => post.Tags)
                        .Include(post => post.Image)
                        .Include(post => post.Category)
                        .First();
        }

        public IEnumerable<Post> GetPostsByCategory(string categoryName)
        {           
            return GetAll()
                        .Where(category => category.Category.Name.Equals(categoryName));                
        }

        public int GetCategoryCounter(IEnumerable<Post> collection, string categoryName)
        {
            return collection
                        .Where(post => post.Category.Name == categoryName)
                        .Count();
        }

        public IEnumerable<Post> GetPostsByText(string text)
        {
            //search in title/short description/description

            return GetAll()
                        .Where(option => option.Description.Contains(text)      ||
                                         option.ShortDescription.Contains(text) ||
                                         option.Title.Contains(text)
                                       );           
        }

        public IEnumerable<Post> GetPostsByTag(string tagName)
        {
            //get all posts which contain tagName
            return GetAll()
                        .Where(option => option.Tags.Any(name => name.Name == tagName));
            
        }
    }
}
