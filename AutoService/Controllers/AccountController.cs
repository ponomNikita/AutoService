using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using AutoService.DAL;
using AutoService.DAL.Models;
using AutoService.Services.Interfaces;
using AutoService.Services.Services;
using AutoService.Services.ViewModels;
using AutoService.WEB.Controllers;

namespace AutoService.Controllers
{
    public class AccountController : BaseController
    {     
        public AccountController()
        {
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
                string modelError;
                var newUser = accountService.CreateUser(model, out modelError);

                if (!string.IsNullOrWhiteSpace(modelError))
                {
                    ModelState.AddModelError("", modelError);
                    return View(model);
                }

                return View("SuccessCreationUser", newUser);
            }
            else
            {
                return !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(returnUrl) : Redirect("~/");
            }
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
            string modelError;
            accountService.Login(model, out modelError);

            if (!string.IsNullOrWhiteSpace(modelError))
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
           
        }

        public ActionResult Logout()
        {
            accountService.Logout();
            return RedirectToAction("Index", "Home");
        }

    }
}