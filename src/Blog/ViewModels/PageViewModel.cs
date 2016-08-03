using Blog.Models;
using Blog.Models.PostViewModels;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class PageViewModel
    {
        public SearchViewModel Search;
        public PagedList<IEnumerable<PostViewModel>, PostViewModel> PostsPerPage;
        public double TotalPosts;
        public double TotalPages;
        public int CurrentPage;
    }
}
