using Blog.Models.Data;
using Blog.Models.PostViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        public Post(PostViewModel viewModel)
        {
            Id = viewModel.Id;
            Title = viewModel.Title;
            ShortDescription = viewModel.ShortDescription;
            Description = viewModel.Description;
            UrlSlug = viewModel.Title.Trim();
            PostedOn = DateTime.UtcNow;
           
        }

        public Post()
        {

        }

        public int Id { get; set; }
       
        public string Title { get; set; }
        
        public string ShortDescription { get; set; }

        public string Author { get; set; }
   
        public string Description { get; set; }

        public string UrlSlug { get; set; }

        public DateTime PostedOn { get; set; }

        public virtual DateTime? Modified { get; set; }

        public bool? IsPublished { get; set; }

        public Category Category { get; set; }

        public ICollection<Tag> Tags { get; set; }

    }
}
