using AutoMapper;
using Blog.Models.Account;
using Blog.Models.PostViewModels;
using Blog.ViewModels;
using Blog.ViewModels.Account;
using Blog.ViewModels.ControlPanelViewModels;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    public static class ModelFactory
    {

       

        public static PostViewModel Create(Post post)
        {
            return Mapper.Map<PostViewModel>(post);

        }

        public static Post Create<T>(T viewModel)
        {
            return Mapper.Map<Post>(viewModel);
        }

        public static ICollection<T> Create<T>(ICollection<Post> posts)
        {
            return Mapper.Map<ICollection<T>>(posts);
        }

        public static Login Create(LoginViewModel viewModel)
        {
            return Mapper.Map<Login>(viewModel);
        }

      

        public static BlogUser Create(RegistrationViewModel viewModel)
        {
            return new BlogUser
            {
                UserName = viewModel.Name,
                Age = viewModel.Age,
                Email = viewModel.Email
            };
        }

        public static ICollection<TagViewModel> Create(ICollection<Tag> tags)
        {
            return Mapper.Map<ICollection<TagViewModel>>(tags);
        }

        public static CategoryViewModel Create(Category category)
        {
            return Mapper.Map<CategoryViewModel>(category);
        }

        public static PageViewModel Create(PagedList<IEnumerable<PostViewModel>, PostViewModel> pagedList)
        {
            return new PageViewModel
            {
                PostsPerPage = pagedList,
                Search = new SearchViewModel()
            };
        }
    }
}
