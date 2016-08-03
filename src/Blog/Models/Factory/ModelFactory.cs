using AutoMapper;
using Blog.Models.Account;
using Blog.Models.PostViewModels;
using Blog.ViewModels;
using Blog.ViewModels.Account;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    public static class ModelFactory
    {

        public static Post Create(PostCreateViewModel viewModel)
        {
            return Mapper.Map<Post>(viewModel);
        }

        public static PostViewModel Create(Post post)
        {
            return Mapper.Map<PostViewModel>(post);

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

           public static PageViewModel Create(Pager pager)
           {
               var result        = Mapper.Map<PageViewModel>(pager);
                   result.Search = new SearchViewModel();
            return result;

           }

    }
}
