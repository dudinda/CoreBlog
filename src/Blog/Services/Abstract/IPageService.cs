using System.Collections.Generic;
using Blog.Models;
using Blog.ViewModels;
using Sakura.AspNetCore;

namespace Blog.Service
{
    public interface IPageService
    {

        int PageSize { get; set; }
        IPagedList<PostViewModel> GetPagedList(IEnumerable<Post> posts, int pageIndex);
    }
}