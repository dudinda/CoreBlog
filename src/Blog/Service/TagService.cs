using Blog.Models.PostViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    public class TagService : ITagService
    {
        private BlogContext context;

        public TagService(BlogContext context)
        {
            this.context = context;
        }

        public void AddTags(ICollection<Tag> tags)
        {
          foreach(var tag in tags)
            {
                if(!string.IsNullOrWhiteSpace(tag.Name))
                {
                    context.Add(tag);
                }
            }            
        }           
    }
}
