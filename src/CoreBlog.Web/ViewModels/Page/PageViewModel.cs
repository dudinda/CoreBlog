using CoreBlog.Web.ViewModels.Post;
using Sakura.AspNetCore;

namespace CoreBlog.Web.ViewModels.Page
{
    public class PageViewModel
    {
        public PageViewModel(IPagedList<PostViewModel> pagedList)
        {
            PagedList = pagedList;
        }

        public IPagedList<PostViewModel> PagedList { get; }

        public SearchViewModel Search { get; set; } = new SearchViewModel();
        
    }
}
