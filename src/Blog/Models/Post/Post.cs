using Blog.Models.Data;
using Blog.Models.PostViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blog.Models
{
    public class Post
    {
      
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

        public bool IsPublished { get; set; }

        public Category Category { get; set; }

        public List<Tag> Tags { get; set; }
       

    }
}
