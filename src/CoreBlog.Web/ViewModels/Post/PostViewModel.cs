using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;


namespace CoreBlog.Web.ViewModels.Post
{
    public class PostViewModel
    {

        public int Id { get; set; }
        
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public DateTime PostedOn { get; set; }

        public DateTime? Modified { get; set; }

        public ImageViewModel Image { get; set; }

        public CategoryViewModel Category { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}