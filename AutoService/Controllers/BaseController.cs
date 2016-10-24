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
using AutoService.Infrastructure.Logger;

namespace AutoService.WEB.Controllers
{
    public abstract class BaseController : Controller
    {
        protected User currentUser;
        protected IAccountService accountService;
        protected ILogger Logger;
        public new HttpContextBase HttpContext
        {
            get
            {
                HttpContextWrapper context =
                    new HttpContextWrapper(System.Web.HttpContext.Current);
                return (HttpContextBase)context;
            }
        }

        public BaseController()
        {
            accountService = new AccountService(HttpContext.User);
            currentUser = accountService.GetCurrentUser();
            Logger = new Logger();
        }

        //protected override void Initialize(RequestContext requestContext)
        //{
        //    base.Initialize(requestContext);
        //    accountService = new AccountService(requestContext.HttpContext.User);
        //    currentUser = accountService.GetCurrentUser();
        //    Logger = new Logger();
        //}
    }
}