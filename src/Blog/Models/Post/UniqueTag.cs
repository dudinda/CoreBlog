using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    public class UniqueTag
    {
        public UniqueTag(string name)
        {
            Name = name;
        }

        public UniqueTag() { }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Counter { get; set; } = 0;
    }
}
