using Blog.ViewModels;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.PostViewModels
{
    public class PostCreateViewModel
    {    
        [Required]
        public string Title { get; set; }

        [Required]
        public  string ShortDescription { get; set; }

        [Required]    
        public  string Description { get; set; }

        [Required]
        public CategoryViewModel Category { get; set; }

        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}
