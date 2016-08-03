using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        [StringLength(64)]
        public string Text { get; set; }
    }
}
