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

        public ICollection<UniqueTag> GetAll()
        {
            var result = context.UniqueTags.ToList();

            return result;
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
    
             
        public bool DeleteTag(int id)
        {
            var result = GetTag(id);

            if (result != null)
            {
                context.Remove(result);
                context.SaveChanges();
                return true;
            }

            return false;
        }

       

        public UniqueTag GetTag(string Name)
        {
            var result = context.UniqueTags
               .Where(option => option.Name == Name)
               .Single<UniqueTag>();

            return result;
        }

        public UniqueTag GetTag(int id)
        {
            //get an existing tag
            var result = context.UniqueTags
                .Where(option => option.Id == id)
                .Single<UniqueTag>();

            return result;
        }
    }
}
