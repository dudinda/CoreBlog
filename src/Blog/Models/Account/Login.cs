using Blog.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class LoginModel
    {
        public LoginModel(LoginViewModel viewModel)
        {
            UserName = viewModel.UserName;
            Password = viewModel.Password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
