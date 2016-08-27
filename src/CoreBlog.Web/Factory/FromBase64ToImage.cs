using CoreBlog.Data.Entities;
using System;
using System.IO;


namespace CoreBlog.Data.Utility
{
    public static class FromBase64ToImage
    {
        public static void ToImage(Image source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ToImageImpl(source); 
        }

        private static void ToImageImpl(Image source)
        {
            var bytes = Convert.FromBase64String(source.Base64);

            File.WriteAllBytes(@"wwwroot\images\" + source.Filename, bytes);
        }


        public static void Delete(Image source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            DeleteImpl(source);
        }

        private static void DeleteImpl(Image source)
        {
            File.Delete(@"wwwroot\images\" + source.Filename);
        }
    }
}
