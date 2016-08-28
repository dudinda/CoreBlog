using CoreBlog.Data.Entities;
using CoreBlog.Web.Factory;
using CoreBlog.Web.ViewModels.Post;
using Microsoft.Extensions.Configuration;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreBlog.Web.Services
{
    public class PageService : IPageService
    {
        private IConfigurationRoot config { get; }

        public PageService(IConfigurationRoot config)
        {
            this.config = config;
        }
      
        public IPagedList<PostViewModel> GetPagedList(IEnumerable<Post> posts, int pageIndex)
        {
            //passing to the factory all posts with attached tags, category and image          
            return ModelFactory
                        .Create<PostViewModel>(posts)
                        .OrderByDescending(time => time.PostedOn)
                        .ToPagedList(Convert.ToInt32(config["Site:PageSize"]), pageIndex); 
        }
    }
}
