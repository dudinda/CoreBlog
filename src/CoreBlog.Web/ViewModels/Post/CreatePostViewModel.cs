using Microsoft.AspNetCore.Html;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Web.ViewModels.Post
{
    public class CreatePostViewModel
    {
        
        public int Id { get; set; }
            
        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public  string Description { get; set; }

        [Required]
        public CategoryViewModel Category { get; set; }

        public ImageViewModel Image { get; set; }

        public ICollection<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}
