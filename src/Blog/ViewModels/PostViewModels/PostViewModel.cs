using Blog.Models.Data;
using Blog.Models.PostViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime PostedOn { get; set; }

        public virtual DateTime? Modified { get; set; }

        public CategoryViewModel Category { get; set; }

        public List<TagViewModel> Tags { get; set; }
    }
}