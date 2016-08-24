using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Web.ViewModels.Account
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
