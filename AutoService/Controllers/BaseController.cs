using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using AutoService.DAL.Models;
using AutoService.Services.Interfaces;
using AutoService.Services.Services;
using System.Web;
using System.Web.Routing;
using AutoService.Logger;

namespace AutoService.WEB.Controllers
{
    public abstract class BaseController : Controller
    {
        protected User currentUser;
        private IAccountService accountService;
        protected ILogger Logger;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            accountService = new AccountService(requestContext.HttpContext.User);
            currentUser = accountService.GetCurrentUser();
            Logger = new Logger.Logger();
        }
    }
}