using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoService.DAL.Models;

namespace AutoService.Security
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public string Roles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            if (Roles == null)
            {
                return true;
            }

            var roles = Roles.Split(new char[]{',', ' '}, StringSplitOptions.RemoveEmptyEntries);
            var userRoles = Security.GetUserRoles(httpContext.User.Identity.Name);

            if (userRoles.Any(t => roles.Any(o => o == t)))
            {
                return true;
            }
                return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                                { 
                                    controller = "Account", 
                                    action = "Login"
                                })
                        );
        }
    }
}