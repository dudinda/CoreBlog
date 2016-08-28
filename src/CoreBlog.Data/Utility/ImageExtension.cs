using CoreBlog.Data.Entities;
using System;
using System.IO;


namespace CoreBlog.Data.Utility
{
    public static class ImageExtension
    {
        public static void ToImage(this Image source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ToImageImpl(source);
        }

        private static void ToImageImpl(this Image source)
        {
            var bytes = Convert.FromBase64String(source.Base64);

            File.WriteAllBytes(@"wwwroot\images\" + source.Filename, bytes);
        }


        public static void Delete(this Image source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            DeleteImpl(source);
        }

        private static void DeleteImpl(this Image source)
        {
            File.Delete(@"wwwroot\images\" + source.Filename);
        }
    }
}
