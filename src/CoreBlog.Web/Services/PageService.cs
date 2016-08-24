using CoreBlog.Data.Entities;
using CoreBlog.Web.Factory;
using CoreBlog.Web.ViewModels.Post;
using Sakura.AspNetCore;
using System.Collections.Generic;
using System.Linq;

namespace CoreBlog.Web.Services
{
    public class PageService : IPageService
    {
        public int PageSize { get; set; } = 5;
      
        public IPagedList<PostViewModel> GetPagedList(IEnumerable<Post> posts, int pageIndex)
        {
            //passing to the factory all posts with attached tags and category            
            return ModelFactory
                        .Create<PostViewModel>(posts)
                        .OrderByDescending(time => time.PostedOn)
                        .ToPagedList(PageSize, pageIndex); 
        }
    }
}
