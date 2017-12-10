using System;
using System.Linq;
using NUnit.Framework;
using AutoService.DAL;
using AutoService.DAL.Models;
using AutoService.Services.Interfaces;
using AutoService.Services.ViewModels;
using NSubstitute;

namespace AutoService.Services.Test
{
    [TestFixture]
    public class ApplicationServiceTest
    {
        private Application[] applications;
        private IApplicationService appService;
        IRepository<Application> sub = Substitute.For<IRepository<Application>>();
        IDateTimeProvider datetimeSub = Substitute.For<IDateTimeProvider>();

        [SetUp]
        public void Init()
        {
            applications = new Application[]
            {
                new Application()
                {
                    Id = 1,
                    Car = new Car
                    {
                        Model = "uaz patriot",
                        RegNumber = "as123d"
                    },

                    CreatedAt = DateTime.Now,
                    CreatedBy = "sys",
                    Date = new DateTime(2016, 10, 17, 15, 30, 0),
                    IsApproved = false,
                    RequestType = 0,
                    Status = 0

                },

                new Application()
                {
                    Id = 2,
                    Car = new Car
                    {
                        Model = "mercedes cls",
                        RegNumber = "ad123d"
                    },
                    CreatedAt = DateTime.Now,
                    CreatedBy = "sys",
                    Date = new DateTime(2016, 10, 17, 16, 30, 0),
                    IsApproved = false,
                    RequestType = 0,
                    Status = 0

                },
            };


            sub.GetAll().Returns(applications.AsQueryable());
            datetimeSub.Now.Returns(new DateTime(2016, 10, 10, 23, 19, 0));
            appService = new ApplicationService(sub, datetimeSub, null);
        }

        [Test]
        [TestCase("2016.10.17 16:30:00", TestName = "Time is busy [1]", ExpectedResult = false)]
        [TestCase("2016.10.17 16:00:00", TestName = "Time is free [2]", ExpectedResult = true)]
        [TestCase("2016.10.09 16:00:00", TestName = "Time has passed [3]", ExpectedResult = false)]
        public bool IsFreeTimeTest(string date)
        {
            DateTime Date;
            DateTime.TryParse(date, out Date);

            return appService.IsFreeTime(Date);
        }

        [Test]
        [TestCase("uaz patriot", TestName = "When car model is uaz patriot", ExpectedResult = 1)]
        [TestCase("mercedes cls", TestName = "When car model is mercedes cls", ExpectedResult = 2)]
        [TestCase("opel", TestName = "When car model is opel", ExpectedResult = -1)]
        public int GetFilteredByCarModelTest(string carModel)
        {
            ApplicationFilter filter = new ApplicationFilter()
            {
                CarModel = carModel
            };

            var applications = appService.GetFiltered(filter);
            var apps = applications as Application[] ?? applications.ToArray();
            if (!apps.Any())
                return -1;

            return apps.FirstOrDefault().Id;
        }

        [Test]
        [TestCase("2016.10.17 16:30:00", TestName = "Fail creation", ExpectedResult = "К сожалению это время занято. Пожалуйста, выберете другую дату или время")]
        [TestCase("2016.12.09 16:00:00", TestName = "Success creation", ExpectedResult = "")]
        [TestCase("", TestName = "Date is null", ExpectedResult = "Поле 'Дата' должно быть заполнено")]
        public string CreateApplicationTest(string date)
        {
            DateTime Date;
            DateTime.TryParse(date, out Date);

            ApplicationEdit newApp = new ApplicationEdit(new Application()
            {
                Id = 1,
                Car = new Car
                {
                    Model = "opel",
                    RegNumber = "gh345q"
                },
                CreatedAt = DateTime.Now,
                CreatedBy = "sys",
                Date = Date,
                IsApproved = false,
                RequestType = 0,
                Status = 0

            });

            return  appService.Create(newApp);
        }

        [Test]
        [TestCase("", TestName = "Car Model is empty", ExpectedResult = "Поле 'Модель автомобиля' должно быть заполнено")]
        [TestCase("opel", TestName = "Car Model is opel", ExpectedResult = "")]
        public string CreateApplicationWhenCarModelIsEmpty(string carModel)
        {
            ApplicationEdit newApp = new ApplicationEdit(new Application()
            {
                Id = 1,
                Car = new Car
                {
                    Model = carModel,
                    RegNumber = "gh345q"
                },
                CreatedAt = DateTime.Now,
                CreatedBy = "sys",
                Date = new DateTime(2016, 12, 17, 16, 30, 0),
                IsApproved = false,
                RequestType = 0,
                Status = 0

            });

            return  appService.Create(newApp);
        }

        [Test]
        [TestCase("", TestName = "Car number is empty", ExpectedResult = "Поле 'Номер автомобиля' должно быть заполнено")]
        [TestCase("q123qw", TestName = "Car number is q123qw", ExpectedResult = "")]
        public string CreateApplicationWhenCarNumberIsEmpty(string carNumber)
        {
            ApplicationEdit newApp = new ApplicationEdit(new Application()
            {
                Id = 1,
                Car = new Car
                {
                    Model = "opel",
                    RegNumber = carNumber
                },
                CreatedAt = DateTime.Now,
                CreatedBy = "sys",
                Date = new DateTime(2016, 12, 17, 16, 30, 0),
                IsApproved = false,
                RequestType = 0,
                Status = 0

            });

            return appService.Create(newApp);
        }
    }
}
