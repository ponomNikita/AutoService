using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NSubstitute;
using NUnit.Framework;
using AutoService.Services.Interfaces;
using AutoService.Services.Services;
using AutoService.DAL;
using AutoService.DAL.Models;

namespace AutoService.Services.Test
{
    [TestFixture]
    class PermissionServiceTests
    {
        IPermissionService service;
        IAutoServiceUnitOfWork uowSub;
        User[] users;
        User_Role[] user_roles;
        Role[] roles;

        [SetUp]
        public void Init()
        {
            users = new User[]
           {
                new User()
                {
                    id = 1,
                    Login = "sys",
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "sys@sys.com"
                },

                new User()
                {
                    id = 2,
                    Login = "Vasya",
                    FirstName = "Vasiliy",
                    LastName = "Pupkin",
                    Email = "vasya@vasya.com"
                },
           };

            user_roles = new User_Role[]
            {
                new User_Role()
                {
                    id = 1,
                    userId = 1,
                    roleId = 1
                },

                new User_Role()
                {
                    id = 2,
                    userId = 1,
                    roleId = 2
                }
            };

            roles = new Role[]
            {
                new Role()
                {
                    id = 1,
                    Code = 1,
                    Description = "Системный администратор",
                    Name = "admin"
                },

                new Role()
                {
                    id = 2,
                    Code = 2,
                    Description = "manager",
                    Name = "manager"
                }
            };

            uowSub = Substitute.For<IAutoServiceUnitOfWork>();
            uowSub.Users.GetAll().Returns(users.AsQueryable());
            uowSub.User_Roles.GetAll().Returns(user_roles.AsQueryable());
            uowSub.Roles.GetAll().Returns(roles.AsQueryable());

            service = new PermissionService(uowSub);
        }

        [TestCase(1, "sys", TestName ="User is admin", ExpectedResult = true)]
        [TestCase(1, "Vasya", TestName = "User is not admin", ExpectedResult = false)]
        [TestCase(1, "Natasha", TestName = "There isn't such user login", ExpectedResult = false)]
        public bool HasRoleTest(int roleCode, string userLogin)
        {
            bool res = service.HasRole(roleCode, userLogin);
            return res;
        }

        [TestCase("sys", TestName = "Get Sys roles", ExpectedResult = "admin,manager")]
        [TestCase("Vasya", TestName = "Get Vasya roles", ExpectedResult = "")]
        [TestCase("Dima", TestName = "There isn't any Dima", ExpectedResult = null)]
        public string GetUserRolesTest(string login)
        {
            string[] roles = service.GetUserRoles(login);

            if (roles == null)
                return null;

            string result = string.Join(",", roles);

            return result;
        }
    }
}
