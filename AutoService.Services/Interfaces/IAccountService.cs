using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL.Models;
using AutoService.Services.ViewModels;

namespace AutoService.Services.Interfaces
{
    public interface IAccountService
    {
        User GetCurrentUser();
        User GetUser(string login);
        User GetUserByLogin(string login);
        User CreateUser(UserViewModel model, out string modelErrorMsg);
        User EditProfile(UserViewModel model, out string modelErrorMsg);
        void Login(LoginViewModel model, out string modelError);
        void Logout();
    }
}
