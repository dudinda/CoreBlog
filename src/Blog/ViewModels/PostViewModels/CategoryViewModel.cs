using System.Collections.Generic;

namespace Blog.Models.PostViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            this.Posts = new List<PostViewModel>();
        }

        public  int Id { get; set; }

        public  string Name { get; set; }

        public  string UrlSlug { get; set; }

        public  string Description { get; set; }

        public virtual ICollection<PostViewModel> Posts { get; set; }
    }
}