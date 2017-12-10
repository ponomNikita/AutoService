using System;
using System.Data.Entity;
using System.Linq;
using AutoService.Services.Interfaces;
using AutoService.DAL;

namespace AutoService.Services.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IAutoServiceUnitOfWork _uow;

        public PermissionService(IAutoServiceUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Вернет есть ли у текущего пользователя запрашиваемая роль 
        /// </summary>
        public bool HasRole(int roleCode, string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login));
            }

            var user = _uow.Users.GetAll()
                .Include(u => u.Roles)
                .FirstOrDefault(t => t.Login.ToUpper() == login.ToUpper());

            if (user == null)
            {
                throw new ArgumentException($"Пользователя с логином {login} не существует");
            }

            return user.Roles.Any(u => u.Code == roleCode);
        }

        public string[] GetUserRoles(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentNullException(nameof(login));
            }

            var user = _uow.Users.GetAll()
                .Include(u => u.Roles)
                .FirstOrDefault(t => t.Login.ToUpper() == login.ToUpper());

            if (user == null)
            {
                throw new ArgumentException($"Пользователя с логином {login} не существует");
            }

            return user.Roles.Select(r => r.Name).ToArray();
        }
    }
}
