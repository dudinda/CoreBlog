using CoreBlog.Data.Context;
using CoreBlog.Web.Services;
using Moq;
using Xunit;

namespace CoreBlog.Test
{
    public class PostServiceTest
    {
        private Mock<IBlogContext> context { get; }

        public PostServiceTest()
        {
            this.context = new Mock<IBlogContext>();
        } 

        [Fact]
        public void ServiceImplementsInterfaceTest()
        { 
            var service = new PostService(context.Object) as IPostService;
            Assert.NotNull(service);           
        }
  
    }
}
