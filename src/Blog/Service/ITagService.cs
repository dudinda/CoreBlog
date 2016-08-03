using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    public interface ITagService
    {           
        void AddTags(ICollection<Tag> tags);       
    }
}
