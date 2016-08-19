using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        [StringLength(64)]
        public string Text { get; set; }
    }
}
