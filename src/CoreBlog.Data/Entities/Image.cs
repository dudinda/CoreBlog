
namespace CoreBlog.Data.Entities
{
    public class Image
    {
        public int id { get; set; }
        public int PostId { get; set; }
        public int Filesize { get; set; }
        public string Filetype { get; set; }
        public string Filename { get; set; }
        public string Base64 { get; set; }
    }
}
