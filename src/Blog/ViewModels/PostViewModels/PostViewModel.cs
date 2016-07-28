using Blog.ViewModels.PostViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.PostViewModels
{
    public class PostViewModel
    {


        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public  string ShortDescription { get; set; }

        [Required]    
        public  string Description { get; set; }
  
        public DateTime? Modified { get; set; }

        public CategoryViewModel Category { get; set; }

        public ICollection<TagViewModel> Tags { get; set; }
    }
}
