using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Services.Interfaces;
using AutoService.DAL;
using AutoService.DAL.Models;

namespace AutoService.Services.Services
{
    public class PermissionService : IPermissionService
    {
        IAutoServiceUnitOfWork uow;

        public PermissionService(IAutoServiceUnitOfWork _uow)
        {
            uow = _uow;
        }

        /// <summary>
        /// Вернет есть ли у текущего пользователя запрашиваемая роль 
        /// </summary>
        public bool HasRole(int roleCode, string currentUserLogin)
        {
            if (string.IsNullOrWhiteSpace(currentUserLogin))
            {
                return false;
            }
            User currentUser = uow.Users.GetAll().FirstOrDefault(t => t.Login.ToUpper() == currentUserLogin.ToUpper());

            if (currentUser == null)
            {
                return false;
            }

            List<int> userRoles = uow.User_Roles.GetAll().Where(t => t.userId == currentUser.id).Select(x => x.roleId).ToList();
 
            return uow.Roles.GetAll().Any(t => userRoles.Contains(t.id) && t.Code == roleCode);
        }

        public string[] GetUserRoles(string login)
        {
            User user = uow.Users.GetAll().FirstOrDefault(t => t.Login.ToUpper() == login.ToUpper());
            if (user == null)
            {
                return null;
            }

            List<int> rolesIds = uow.User_Roles.GetAll().Where(t => t.userId == user.id).Select(x => x.roleId).ToList();
            var roles =
                uow.Roles.GetAll().Where(t => rolesIds.Any(o => o == t.id)).Select(x => x.Name).ToArray();

            return roles;
        }
    }
}
