using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using AutoService.DAL;
using AutoService.DAL;
using AutoService.DAL.FilterModel;
using AutoService.DAL.Models;
using AutoService.Services.ViewModels;
using AutoService.Security;
using AutoService.Services.Enums;
using AutoService.Services.Interfaces;
using AutoService.Services;
using AutoService.Services.Services;
using AutoService.WEB.Controllers;

namespace AutoService.Controllers
{
    public class ApplicationController : BaseController
    {
        private IApplicationService appService;
        private IDateTimeProvider timeProvider;

        public ApplicationController()
            :base()
        {
            timeProvider = ServicesFactory.CreateDateTimeProvider();
            appService = ServicesFactory.CreateApplicationService(new Repository<Application>(DBContext.GetDBContext()), timeProvider, currentUser);
        }

        [HttpGet]
        [AuthorizeUser]
        public ActionResult Create(int requestType = 0)
        {
            ApplicationEdit newApplication = new ApplicationEdit()
            {
                RequestType = requestType,
                Status = (int)ApplicationStatus.WaitForApprove,
                IsApproved = false
            };
            return View(newApplication);
        }

        [HttpPost]
        public ActionResult Create(ApplicationEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var modelError = appService.Create(model);
            if (!string.IsNullOrWhiteSpace(modelError))
            {
                ModelState.AddModelError("", modelError);
                return View(model);
            }

            //TODO Сделать редирект на список заявок клиента, когда он будет реализован
            return RedirectToAction("Index", "Application", new { CreatedBy = currentUser.Login});
        }

        [HttpGet]
        [AuthorizeUser]
        public ActionResult Index(ApplicationFilter filter)
        {
            ApplicationContent model = new ApplicationContent()
            {
                Filter = filter,
                Items = GetFiltredContent(filter)
            };
            return View(model);
        }

        public IEnumerable<Application> GetFiltredContent(ApplicationFilter filter)
        {
            return appService.GetFiltered(filter);
        }
    }
}