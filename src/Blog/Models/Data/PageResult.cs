using Blog.Models.PostViewModels;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    
    public class PageResult
    {
        public PageResult(BlogContext context, int page, int pageSize = 5)
        {
            try
            {
                TotalPosts = context.Posts.Count();
                CurrentPage = page;
                TotalPages = Math.Ceiling(TotalPosts / pageSize);
                PostsPerPage = context
                    .Posts
                    .OrderBy(x => x.PostedOn)
                    .ToList()
                    .Reverse<Post>()
                    .ToPagedList(pageSize, CurrentPage);
            }
            catch(ArgumentNullException)
            {
                TotalPosts = 0;
                CurrentPage = 1;
            }
        }

        public PagedList<IEnumerable<Post>, Post> PostsPerPage;
        public double TotalPosts;
        public double TotalPages;
        public int CurrentPage; 

    }
}
