using Blog.Models.PostViewModels;
using System;
using System.Collections.Generic;


namespace Blog.ViewModels.ControlPanelViewModels
{
    public class PostControlPanelViewModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime PostedOn { get; set; }
        public bool IsPublished { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}
