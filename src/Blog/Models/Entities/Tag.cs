

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blog.Models.Data
{
    public class Tag
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Name { get; set; }
    }
}
