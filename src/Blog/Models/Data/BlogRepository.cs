using Blog.Models.PostViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    public class BlogRepository
    {
        private BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Add(post);
        }

        public void SaveAll()
        {
            _context.SaveChanges();
        }

        public Post FindPostById(int id)
        {
            var result = _context.Posts
                .Where(option => option.Id == id);
                

            return result.First();
        }
    }
}
