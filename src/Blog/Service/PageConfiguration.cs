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
    public static class PageConfiguration
    {
        public static int PageSize { get; set; } = 5;
        public static int InitialPage { get; set; } = 1;

        public static PagedList<IEnumerable<PostViewModel>, PostViewModel> GetPagedList(ICollection<Post> posts)
        {
            //passing to the factory all posts with attached tags and category
            var result = ModelFactory.Create(posts)
                .OrderByDescending(time => time.PostedOn)
                .ToPagedList(PageSize, InitialPage);

            return result;
        }
    }
}
