using Sakura.AspNetCore;
using System.Collections.Generic;

namespace Blog.ViewModels
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
