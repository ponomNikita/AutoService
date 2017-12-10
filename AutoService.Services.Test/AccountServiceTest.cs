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


namespace AutoService.Services.Test
{
    [TestFixture]
    public class AccountServiceTest
    {
        IAccountService _service;
        IAutoServiceUnitOfWork _uowSub;
        User[] _users;
        Role[] _roles;
        private UserViewModel _newUser;

        [SetUp]
        public void Init()
        {

            _roles = new[]
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

            _newUser = new UserViewModel()
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
            _users = new User[]
           {
                new User()
                {
                    Id = 1,
                    Login = "sys",
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "sys@sys.com",
                    Roles = new List<Role> { _roles[0] }
                },

                new User()
                {
                    Id = 2,
                    Login = "Vasya",
                    FirstName = "Vasiliy",
                    LastName = "Pupkin",
                    Email = "vasya@vasya.com",
                    Roles = new List<Role> { _roles[1] }
                },
           };

            _uowSub = Substitute.For<IAutoServiceUnitOfWork>();
            _uowSub.Users.GetAll().Returns(_users.AsQueryable());
            _uowSub.Roles.GetAll().Returns(_roles.AsQueryable());

            _service = new AccountService(_uowSub);
        }

        [Test]
        [TestCase("Vasya", "vasya@vasya.com", TestName = "Same login", ExpectedResult = "Пользователь с таким логином уже зарегистрирован")]
        [TestCase("Vasya1", "vasya@vasya.com", TestName = "Same email", ExpectedResult = "Пользователь с такой электронной почтой уже зарегистрирован")]
        [TestCase("Vasya1", "vasya1@vasya.com", TestName = "Success creation", ExpectedResult = "")]

        public string CreateUserTest(string login, string email)
        {
            _newUser.Login = login;
            _newUser.Email = email;
            string msg;
            _service.CreateUser(_newUser, out msg);

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
