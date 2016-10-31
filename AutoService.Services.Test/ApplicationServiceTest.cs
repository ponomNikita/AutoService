using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoService.Services;
using NUnit.Framework;
using AutoService.DAL;
using AutoService.DAL.Models;
using AutoService.Services.Interfaces;
using AutoService.Services;
using NSubstitute;
using NSubstitute.Core;

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
                    id = 1,
                    CarModel = "uaz patriot",
                    CarNumber = "as123d",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "sys",
                    Date = new DateTime(2016, 10, 17, 15, 30, 0),
                    IsApproved = false,
                    RequestType = 0,
                    Status = 0

                },

                new Application()
                {
                    id = 2,
                    CarModel = "mercedes cls",
                    CarNumber = "ad123d",
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
    }
}
