using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoService.DAL.Models;
using AutoService.DAL;

namespace AutoService.Security
{
    public static class Security
    {
        
        /// <summary>
        /// Вернет есть ли у текущего пользователя запрашиваемая роль 
        /// </summary>
        public static bool HasRole(int roleCode, string currentUserLogin)
        {
            using (var uow = new UnitOfWork())
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

                return uow.Roles.Any(t => userRoles.Contains(t.id) && t.Code == roleCode);
            }
        }
    }
}