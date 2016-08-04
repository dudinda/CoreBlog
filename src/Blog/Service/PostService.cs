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
        public static int PageSize { get; set; } = 5;
        public static int InitialPage { get; set; } = 1;

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

        public ICollection<Post> GetAll()
        {
            //attach tags and category

            var result = AttachTags(context.Posts.ToList());
            
            return result;
        }
   
        public Post FindPostById(int id)
        {
            var result = context.Posts
                .Where(option => option.Id == id)
                .Single<Post>();
                
            return AttachTags(result);
        }

        public Post FindPostBySlug(string slug)
        {
            var result = context.Posts
                .Where(option => option.UrlSlug == slug)
                .First<Post>();

            return result;
        }

        public ICollection<Post> FindPostsByText(string text)
        {
            //search in title/short description/description

            var result = context.Posts
                .Where(option => option.Description.Contains(text)      ||
                                 option.ShortDescription.Contains(text) ||
                                 option.Title.Contains(text)            
                                 );

            return result.ToList();
               
        }

        public ICollection<Post> GetPostByTag(string tagName)
        {
            //get all posts which contain tagName
            var result = GetAll()
                .Where( option => option.Tags.All(name => name.Name == tagName) )
                .ToList<Post>();

            return result;
            
        }


        public Post AttachTags(Post post)
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

        public ICollection<Post> AttachTags(ICollection<Post> posts)
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
