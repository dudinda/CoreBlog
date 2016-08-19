using Sakura.AspNetCore;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class PageViewModel
    {
        public PagedList<IEnumerable<PostViewModel>, PostViewModel> PostsPerPage { get; set; }
        public SearchViewModel Search { get; set; } = new SearchViewModel();
        
    }
}
