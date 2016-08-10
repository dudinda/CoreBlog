using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Factory
{
    public static class ExtensionUtility
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (count <= 0) yield break;


            var queue = new Queue<T>(count);

            foreach (var item in source)
            {
                if (queue.Count == count)
                {
                    queue.Dequeue();
                }                   
                queue.Enqueue(item);
            }

            foreach (var item in queue)
                yield return item;
        }
    }
}
