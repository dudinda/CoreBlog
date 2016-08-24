using Blog.Controllers;
using Blog.Controllers.Web;
using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Blog.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoreBlog.Test
{
    public class ErrorsControllerTest
    {
        private ErrorsController controller { get; set; }

        public ErrorsControllerTest()
        {
            this.controller = new ErrorsController();
        }


        [Theory]
        [InlineData(404)]
        [InlineData(500)]
        [InlineData(200)]
        public void ViewDataNotNullTest(int error)
        {
            var result = controller.Error(error) as PartialViewResult;       
            Assert.NotNull(result.ViewData["Code"]);
            Assert.NotNull(result.ViewData["Message"]);
        }

        [Fact]
        public void ViewDataInternalServerTest()
        {
            var result = controller.Error(500) as PartialViewResult;
            Assert.Equal(500, result.ViewData["Code"]);
            Assert.Equal("Internal server error!", result.ViewData["Message"]);
        }

        [Fact]
        public void ViewDataNotFoundTest()
        {
            var result = controller.Error(404) as PartialViewResult;
            Assert.Equal(404, result.ViewData["Code"]);
            Assert.Equal("Page not found!", result.ViewData["Message"]);
        }

        [Fact]
        public void ViewDataOthersTest()
        {
            var result = controller.Error(200) as PartialViewResult;
            Assert.Equal(200, result.ViewData["Code"]);
            Assert.Equal("Something went wrong... try again later!", result.ViewData["Message"]);
        }


    }
}
