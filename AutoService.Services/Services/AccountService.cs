using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoService.DAL;
using AutoService.Services.Interfaces;
using AutoService.DAL.Models;

namespace AutoService.Services.Services
{
    public class AccountService : IAccountService
    {
        private IPrincipal User;

        public AccountService(IPrincipal _user)
        {
            User = _user;
        }

        public User GetCurrentUser()
        {
            using (var uow = new AutoServiceUnitOfWork())
            {
                if (User == null)
                    return null;

                return uow.Users.GetAll().FirstOrDefault(t => t.Login == User.Identity.Name);
            }
        }
    }
}
