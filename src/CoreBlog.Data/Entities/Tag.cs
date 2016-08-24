
namespace CoreBlog.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Name { get; set; }
    }
}
