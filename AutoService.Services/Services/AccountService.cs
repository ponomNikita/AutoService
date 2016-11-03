using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;
using AutoService.DAL;
using AutoService.Services.Interfaces;
using AutoService.DAL.Models;
using AutoService.Infrastructure.Logger;
using AutoService.Services.ViewModels;

namespace AutoService.Services.Services
{
    public class AccountService : IAccountService
    {
        private IPrincipal User;
        private ILogger Logger;
        private IAutoServiceUnitOfWork uow;

        public AccountService(IPrincipal _user)
        {
            uow = new AutoServiceUnitOfWork();
            User = _user;
            Logger = new Logger();
        }

        public User GetCurrentUser()
        {
            if (User == null)
                return null;

            return uow.Users.GetAll().FirstOrDefault(t => t.Login == User.Identity.Name);
        }

        public User GetUserByLogin(string login)
        {
            return uow.Users.GetAll().FirstOrDefault(u => u.Login.Equals(login));
        }

        public User CreateUser(UserViewModel model, out string modelErrorMsg)
        {
            if (uow.Users.GetAll().Any(x => x.Login.ToUpper() == model.Login.ToUpper()))
            {
                Logger.Error("Попытка регистрации пользователя с существующим логином");
                modelErrorMsg = "Пользователь с таким логином уже зарегистрирован";
                return null;
            }

            if (uow.Users.GetAll().Any(x => x.Email.ToUpper() == model.Email.ToUpper()))
            {
                Logger.Error("Попытка регистрации пользователя с существующим email");
                modelErrorMsg = "Пользователь с такой электронной почтой уже зарегистрирован";
                return null;
            }

            User newUser = new User();
            newUser.Login = model.Login;
            newUser.Password = GetHashString(model.Password);
            newUser.FirstName = model.FirstName;
            newUser.LastName = model.LastName;
            newUser.Email = model.Email;
            newUser.Address = model.Address;
            newUser.PhoneNumber = model.PhoneNumber;

            Logger.Info(string.Format("Попытка регистрации нового пользователя {0}...", newUser.Login));
            uow.Users.Create(newUser);
            uow.Save();
            Logger.Info("Успешно!");

            modelErrorMsg = string.Empty;
            return newUser;
        }

        public void Login(LoginViewModel model, out string modelError)
        {
            var password = GetHashString(model.Password);
            if (uow.Users.GetAll().Any(x => x.Login.ToUpper() == model.Login.ToUpper() && x.Password.ToUpper() == password.ToUpper()))
            {
                User currUser = uow.Users.GetAll().FirstOrDefault(x => x.Login.ToUpper() == model.Login.ToUpper() && x.Password.ToUpper() == password.ToUpper());
                FormsAuthentication.SetAuthCookie(currUser.Login, model.RememberMe);

                Logger.Info(string.Format("Выполнен вход пользователем {0}", currUser.Login));
                modelError = null;    
            }
            else
            {
                modelError = "Пользователя с таким логином и паролем не существует";
                Logger.Info("Выполнена попытка входа с несуществующим логином или паролем");
            }
        }

        public void Logout()
        {
            Logger.Info(string.Format("Пользователь {0} вышел из системы", GetCurrentUser().Login));
            FormsAuthentication.SignOut();
        }

        private string GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
