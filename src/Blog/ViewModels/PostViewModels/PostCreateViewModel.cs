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
        public PostCreateViewModel()
        {
            this.Tags = new List<TagViewModel>()
            {
                new TagViewModel(),
                new TagViewModel(),
                new TagViewModel(),
                new TagViewModel(),
                new TagViewModel()
            };
        }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public  string ShortDescription { get; set; }

        [Required]    
        public  string Description { get; set; }

        
        public CategoryViewModel Category { get; set; }

        public IList<TagViewModel> Tags { get; set; }
    }
}
