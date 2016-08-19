using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.ViewModels;
using Sakura.AspNetCore;
using Blog.Models.Data;

namespace Blog.Service
{
    public class PageService : IPageService
    {
        public int PageSize { get; set; } = 5;
      
        public PagedList<IEnumerable<PostViewModel>, PostViewModel> GetPagedList(IEnumerable<Post> posts, int pageIndex)
        {
            //passing to the factory all posts with attached tags and category
            var result = ModelFactory
                .Create<PostViewModel>(posts)
                .OrderByDescending(time => time.PostedOn)
                .ToPagedList(PageSize, pageIndex);

            return result;
        }
    }
}
