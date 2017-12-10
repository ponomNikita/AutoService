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
        Role[] roles;

        [SetUp]
        public void Init()
        {

            roles = new Role[]
            {
                new Role()
                {
                    Id = 1,
                    Code = 1,
                    Description = "Системный администратор",
                    Name = "admin"
                },

                new Role()
                {
                    Id = 2,
                    Code = 2,
                    Description = "manager",
                    Name = "manager"
                }
            };

            users = new User[]
           {
                new User()
                {
                    Id = 1,
                    Login = "sys",
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "sys@sys.com",
                    Roles = new List<Role>
                    {
                        roles[0], roles[1]
                    }
                },

                new User()
                {
                    Id = 2,
                    Login = "Vasya",
                    FirstName = "Vasiliy",
                    LastName = "Pupkin",
                    Email = "vasya@vasya.com",
                    Roles = new List<Role>()
                },
           };

            uowSub = Substitute.For<IAutoServiceUnitOfWork>();
            uowSub.Users.GetAll().Returns(users.AsQueryable());
            uowSub.Roles.GetAll().Returns(roles.AsQueryable());

            service = new PermissionService(uowSub);
        }

        [TestCase(1, "sys", TestName ="User is admin", ExpectedResult = true)]
        [TestCase(1, "Vasya", TestName = "User is not admin", ExpectedResult = false)]
        public bool HasRoleTest(int roleCode, string userLogin)
        {
            bool res = service.HasRole(roleCode, userLogin);
            return res;
        }

        public void HasRolesWhenLoginIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => service.HasRole(1, "Dima"));
        }

        [TestCase("sys", TestName = "Get Sys roles", ExpectedResult = "admin,manager")]
        [TestCase("Vasya", TestName = "Get Vasya roles", ExpectedResult = "")]
        public string GetUserRolesTest(string login)
        {
            string[] roles = service.GetUserRoles(login);

            if (roles == null)
                return null;

            string result = string.Join(",", roles);

            return result;
        }

        public void GetUsersRolesWhenLoginIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => service.GetUserRoles("Dima"));
        }
    }
}
