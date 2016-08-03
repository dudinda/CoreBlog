using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Blog.ViewModels;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    
    public class Pager
    {
        public Pager(ICollection<Post> Posts,  int page = 1, int pageSize = 5)
        {

                TotalPosts = Posts.Count();
                CurrentPage = page;
                TotalPages = Math.Ceiling(TotalPosts / pageSize);
                PostsPerPage = Posts
                    .OrderBy(post => post.PostedOn)
                    .ToList()
                    .Reverse<Post>()
                    .ToPagedList(pageSize, CurrentPage);
                     
        }

        public Pager()
        {

        }

        public PagedList<IEnumerable<Post>, Post> PostsPerPage;
        public double TotalPosts;
        public double TotalPages;
        public int CurrentPage;  
    }
}
