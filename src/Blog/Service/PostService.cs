using Blog.Models.Factory;
using Blog.Models.PostViewModels;
using System.Collections.Generic;
using System.Linq;

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

        public void RemovePost(Post post)
        {
            context.Remove(post);
        } 

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }

        public IEnumerable<Post> GetAllUnpublished()
        {
            return Attach(context.Posts.Where(post => !post.IsPublished));
        }

        public IEnumerable<Post> GetAll()
        {
            //attach tags and category
            return Attach(context.Posts.Where(post => post.IsPublished));
        }

        public void UpdatePost(Post post)
        {
            context.Update(post);
        }
 
        public IEnumerable<Post> GetLatest(IEnumerable<Post> posts, int count)
        {
            return posts.TakeLast(5).OrderByDescending(post => post.PostedOn);
        }
            

        public Post GetPostById(int id)
        {
            var result = context.Posts
                .Where(option => option.Id == id)?
                .Single<Post>();
                
            return Attach(result);
        }

        public Post GetPostBySlug(string slug)
        {
            var result = context.Posts
                .Where(option => option.UrlSlug == slug)
                .First<Post>();

            return result;
        }

        public IEnumerable<Post> GetPostsByCategory(string categoryName)
        {           
            return GetAll().Where(category => category.Category.Name.Equals(categoryName));                
        }

        public int GetCategoryCounter(IEnumerable<Post> collection, string categoryName)
        {
            return collection.Where(post => post.Category.Name == categoryName).Count();
        }

        public IEnumerable<Post> GetPostsByText(string text)
        {
            //search in title/short description/description

            var result = GetAll().Where(option => option.Description.Contains(text)      ||
                                                  option.ShortDescription.Contains(text) ||
                                                  option.Title.Contains(text)
                                       );

            return result;
               
        }

        public IEnumerable<Post> GetPostsByTag(string tagName)
        {
            //get all posts which contain tagName
            return GetAll().Where(option => option.Tags.Any(name => name.Name == tagName));
            
        }


        private Post Attach(Post post)
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
    

        private IEnumerable<Post> Attach(IEnumerable<Post> posts)
        {
            var result = posts;

            foreach (var post in result)
            {
                //attach tags to posts

                post.Tags = context.Tags.Where(option => option.PostId == post.Id).ToList();
              

                //attach category to each post

                post.Category = context.Categories
                    .Where(option => option.PostId == post.Id)
                    .Single<Category>();
            }

            return result;
        }
    }
}
