using Blog.ViewModels;
using Blog.ViewModels.PostViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.PostViewModels
{
    public class CreatePostViewModel
    {    
        [Required]
        public string Title { get; set; }

        [Required]
        public  string ShortDescription { get; set; }

        [Required]    
        public  string Description { get; set; }

        [Required]
        public CategoryViewModel Category { get; set; }
        
        public ImageViewModel Image { get; set; }

        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}
