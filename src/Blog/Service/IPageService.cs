using System.Collections.Generic;
using Blog.Models;
using Blog.ViewModels;
using Sakura.AspNetCore;

namespace Blog.Service
{
    public interface IPageService
    {
        int InitialPage { get; set; }
        int PageSize { get; set; }

        PagedList<IEnumerable<PostViewModel>, PostViewModel> GetPagedList(ICollection<Post> posts);
    }
}