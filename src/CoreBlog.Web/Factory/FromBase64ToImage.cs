using CoreBlog.Web.ViewModels.Post;
using System;
using System.IO;


namespace CoreBlog.Data.Utility
{
    public static class FromBase64ToImage
    {
        public static void ToImage(ImageViewModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ToImageImpl(source); 
        }

        private static void ToImageImpl(ImageViewModel source)
        {
            var bytes = Convert.FromBase64String(source.Base64);

            File.WriteAllBytes("wwwroot\\images\\" + source.Filename, bytes);
        }
    }
}
