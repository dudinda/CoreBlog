using AutoMapper;
using Blog.Controllers;
using Blog.Models.Account;
using Blog.Models.AccountViewModels;
using Blog.Models.PostViewModels;
using Blog.ViewModels;
using Sakura.AspNetCore;
using System.Collections.Generic;

namespace Blog.Models.Data
{
    public static class ModelFactory
    {
  
        public static T Create<T>(Post post)
        {
            return Mapper.Map<T>(post);
        }

        public static BlogUser Create(UserControlPanelViewModel viewModel)
        {
            return Mapper.Map<BlogUser>(viewModel);
        }

        public static UserControlPanelViewModel Create(BlogUser user)
        {
            return Mapper.Map<UserControlPanelViewModel>(user);
        }

        public static Post Create(Post post, CreatePostViewModel viewModel)
        {
            return Mapper.Map(viewModel, post);
        }

        public static Post Create(CreatePostViewModel viewModel)
        {
            return Mapper.Map<Post>(viewModel);
        }

        public static Post Create(PostViewModel viewModel)
        {
            return Mapper.Map<Post>(viewModel);
        }

        public static IEnumerable<T> Create<T>(IEnumerable<Post> posts)
        {
            return Mapper.Map<IEnumerable<T>>(posts);
        }

        public static IEnumerable<UserControlPanelViewModel> Create(List<BlogUser> blogUser)
        {
            return Mapper.Map<IEnumerable<UserControlPanelViewModel>>(blogUser);
        }

        public static BlogUser Create(RegistrationViewModel viewModel)
        {
            return Mapper.Map<BlogUser>(viewModel);
        }

        public static IEnumerable<TagViewModel> Create(IEnumerable<Tag> tags)
        {
            return Mapper.Map<IEnumerable<TagViewModel>>(tags);
        }

        public static CategoryViewModel Create(Category category)
        {
            return Mapper.Map<CategoryViewModel>(category);
        }

        public static PageViewModel Create(IPagedList<PostViewModel> pagedList)
        {
            return new PageViewModel(pagedList);
        }
    }
}
