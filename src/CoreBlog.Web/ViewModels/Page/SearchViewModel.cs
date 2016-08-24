using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Web.ViewModels.Page
{
    public class SearchViewModel
    {
        [Required]
        [StringLength(64)]
        public string Text { get; set; }
    }
}
