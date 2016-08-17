using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; } 
    }
}
