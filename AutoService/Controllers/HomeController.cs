using System.Web.Mvc;
using AutoService.Enums;
using AutoService.Services;
using AutoService.Services.Interfaces;

namespace AutoService.Controllers
{
    public class HomeController : Controller
    {
        private IPermissionService permissionService;

        public HomeController()
        {
            permissionService = ServicesFactory.CreatePermissionService();
        }

        public ActionResult Index()
        {
            var userLogin = User.Identity.Name;
            if (!string.IsNullOrWhiteSpace(userLogin) && User.Identity.IsAuthenticated
                && permissionService.HasRole((int) Roles.Admin, User.Identity.Name))
            {
                return RedirectToAction("Index", "Application");
            }

            return View();
        }
    }
}