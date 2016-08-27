using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Data.Utility
{
    public static class StringExtension
    {
        public static bool Contains(this string source, string toSearch, StringComparison cmp)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ContainsImpl(source, toSearch, cmp);
        }


        private static bool ContainsImpl(this string source, string toSearch, StringComparison cmp)
        {
            return source.IndexOf(toSearch, cmp) >= 0;
        }
    }
}
