using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using AutoService.DAL;
using AutoService.DAL.Models;
using AutoService.Services.ViewModels;
using AutoService.Logger;

namespace AutoService.Controllers
{
    public class AccountController : Controller
    {
        UnitOfWork uow;
        ILogger Logger;
        
        public AccountController()
        {
            uow = new UnitOfWork();
            Logger = new AutoService.Logger.Logger();
        }
        
        [HttpGet]
        public ActionResult CreateUser()
        {
            UserViewModel model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateUser(UserViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if(uow.Users.GetAll().Any(x => x.Login.ToUpper() == model.Login.ToUpper()))
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже зарегистрирован");
                    Logger.Error("Попытка регистрации пользователя с существующим логином");
                    return View(model);
                }

                if (uow.Users.GetAll().Any(x => x.Email.ToUpper() == model.Email.ToUpper()))
                {
                    ModelState.AddModelError("", "Пользователь с такой электронной почтой уже зарегистрирован");
                    Logger.Error("Попытка регистрации пользователя с существующим email");
                    return View(model);
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

                return View("SuccessCreationUser", newUser);
            }
            else
            {
                return !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(returnUrl) : Redirect("~/");
            }
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var password = GetHashString(model.Password);
            if (uow.Users.GetAll().Any(x => x.Login.ToUpper() == model.Login.ToUpper() && x.Password.ToUpper() == password.ToUpper()))
            {
                User currentUser = uow.Users.GetAll().FirstOrDefault(x => x.Login.ToUpper() == model.Login.ToUpper() && x.Password.ToUpper() == password.ToUpper());
                FormsAuthentication.SetAuthCookie(currentUser.Login, model.RememberMe);

                Logger.Info(string.Format("Выполнен вход пользователем {0}", currentUser.Login));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Пользователя с таким логином и паролем не существует");
                Logger.Info("Выполнена попытка входа с несуществующим логином или паролем");
                return View();
            }
        }

        public ActionResult Logout()
        {
            Logger.Info(string.Format("Пользователь {0} вышел из системы", User.Identity.Name));
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            uow.Dispose();
            base.Dispose(disposing);
        }

    }
}