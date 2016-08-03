using Blog.Models.PostViewModels;
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

        public PostService()
        {

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
            //search 

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

                post.Tags = context.Tags
                    .Where(option => option.PostId == post.Id)
                    .ToList();

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

                post.Category = context.Categories
                    .Where(option => option.PostId == post.Id)
                    .Single<Category>();
            }

            return result;
        }


        public Pager GetPagedPosts(ICollection<Post> posts, int page = 1, int pageSize = 5)
        {    
            return new Pager( posts ,  page, pageSize );
        }
      
    }
}
