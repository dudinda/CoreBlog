using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.AccountViewModels
{
    public class ContactViewModel
    {

        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
     
    }
}
