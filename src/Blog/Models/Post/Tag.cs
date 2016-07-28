using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.PostViewModels
{
    public class Tag
    {
        public Tag() { }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Counter { get; set; }
    }
}
