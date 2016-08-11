using Blog.Models.Factory;
using Blog.Models.PostViewModels;
using Blog.ViewModels;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    public class PostService : IPostService
    {
        private BlogContext context;

        public PostService(BlogContext context)
        {
            this.context = context;
        }

    
        public void AddPost(Post post)
        {
            context.Add(post);
        }

        public void SaveAll()
        {
            context.SaveChanges();
        }

        public ICollection<Post> GetAllUnpublished()
        {
            var result = AttachTags(context.Posts
                                           .Where(post => !post.IsPublished)
                                           .ToList() );
            return result;
        }

        public ICollection<Post> GetAll()
        {
            //attach tags and category
            var result = AttachTags(context.Posts.ToList());
            
            return result;
        }

        public ICollection<Post> GetLatest(ref ICollection<Post> posts, int count)
        {
            var result = posts.TakeLast(5)
                        .OrderByDescending(post => post.PostedOn)
                        .ToList();

            return result;
        }
            

        public Post GetPostById(int id)
        {
            var result = context.Posts
                .Where(option => option.Id == id)
                .Single<Post>();
                
            return AttachTags(result);
        }

        public Post GetPostBySlug(string slug)
        {
            var result = context.Posts
                .Where(option => option.UrlSlug == slug)
                .First<Post>();

            return result;
        }

        public ICollection<Post> GetPostsByCategory(string categoryName)
        {
            var result = GetAll()
                .Where(category => category.Category.Name.Equals(categoryName))
                .ToList<Post>();

            return result;
                
        }

        public ICollection<Post> GetPostsByText(string text)
        {
            //search in title/short description/description

            var result = GetAll().Where(option => option.Description.Contains(text)      ||
                                                  option.ShortDescription.Contains(text) ||
                                                  option.Title.Contains(text)            
                                       ).ToList<Post>();

            return result;
               
        }

        public ICollection<Post> GetPostsByTag(string tagName)
        {
            //get all posts which contain tagName
            var result = GetAll()             
                .Where( option => option.Tags.Any(name => name.Name == tagName) )
                .ToList<Post>();

            return result;
            
        }


        private Post AttachTags(Post post)
        {         

            //attach tags to post
                post.Tags = context.Tags
                    .Where(option => option.PostId == post.Id)
                    .ToList();

            //attach category to post
                post.Category = context.Categories
                    .Where(option => option.PostId == post.Id)
                    .Single<Category>();

            return post;
        }
    

        private ICollection<Post> AttachTags(ICollection<Post> posts)
        {
            var result = posts;

            foreach (var post in result)
            {
                //attach tags to posts

                post.Tags = context.Tags
                    .Where(option => option.PostId == post.Id)
                    .ToList();

                //attach category to each post

                post.Category = context.Categories
                    .Where(option => option.PostId == post.Id)
                    .Single<Category>();
            }

            return result;
        }
    }
}
