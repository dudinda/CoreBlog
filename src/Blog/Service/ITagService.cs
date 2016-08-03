using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    public interface ITagService
    {
     
        UniqueTag GetTag(int id);
        UniqueTag GetTag(string name);
        void AddTags(ICollection<Tag> tags);
        bool DeleteTag(int id);
    }
}
