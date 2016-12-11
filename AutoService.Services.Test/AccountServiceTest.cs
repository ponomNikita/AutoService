using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.DAL;
using AutoService.DAL.Models;
using AutoService.Services.Interfaces;
using AutoService.Services.Services;
using AutoService.Services.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NSubstitute;
using NUnit.Framework.Internal.Filters;


namespace AutoService.Services.Test
{
    [TestFixture]
    public class AccountServiceTest
    {
        IAccountService service;
        IAutoServiceUnitOfWork uowSub;
        User[] users;
        User_Role[] user_roles;
        Role[] roles;
        private UserViewModel newUser;

        [SetUp]
        public void Init()
        {
            newUser = new UserViewModel()
            {
                id = 0,
                Login = "Vasya",
                FirstName = "System",
                LastName = "Admin",
                Email = "vasya@vasya.com",
                Address = "NN",
                ConfirmPassword = "Q",
                Password =  "Q",
                IsCreate = true
            };
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

            service = new AccountService(uowSub);
        }

        [Test]
        [TestCase("Vasya", "vasya@vasya.com", TestName = "Same login", ExpectedResult = "Пользователь с таким логином уже зарегистрирован")]
        [TestCase("Vasya1", "vasya@vasya.com", TestName = "Same email", ExpectedResult = "Пользователь с такой электронной почтой уже зарегистрирован")]
        [TestCase("Vasya1", "vasya1@vasya.com", TestName = "Success creation", ExpectedResult = "")]

        public string CreateUserTest(string login, string email)
        {
            newUser.Login = login;
            newUser.Email = email;
            string msg;
            service.CreateUser(newUser, out msg);

            return msg;
        }

        [Test]
        [TestCase("qwer", TestName = "qwer -> 9ffbd4b2c991ab3adf0557e013913f5d", ExpectedResult = "9ffbd4b2c991ab3adf0557e013913f5d")]
        [TestCase("qwert", TestName = "qwert -> 50b3f4751aa27e17c4bf6d3f85f68699", ExpectedResult = "50b3f4751aa27e17c4bf6d3f85f68699")]

        public string GetHashStringTest(string s)
        {
            string hash = AccountService.GetHashString(s);
            return hash;
        }
    }
}
