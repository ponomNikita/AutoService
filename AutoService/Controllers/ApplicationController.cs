using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using AutoService.DAL;
using AutoService.DAL.FilterModel;
using AutoService.DAL.Models;
using AutoService.Logger;
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
        private IAutoServiceUnitOfWork uow;
        private IApplicationService appService;
        private IDateTimeProvider timeProvider;

        public ApplicationController()
        {
            timeProvider = new DateTimeProvider();
            uow = new AutoServiceUnitOfWork();
            appService = new ApplicationService(uow.Applications, timeProvider);
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

            Application newItem = new Application();
            model.Copy(newItem);
            newItem.CreatedAt = timeProvider.Now;
            newItem.CreatedBy = currentUser.Login;

            if (!appService.IsFreeTime(newItem.Date))
            {
                ModelState.AddModelError("", "К сожалению это время занято. Пожалуйста, выберете другую дату или время");
                return View(model);
            }

            Logger.Info("Запись в бд новой заявки...");
            uow.Applications.Create(newItem);
            uow.Save();
            Logger.Info("Успешно!");

            //TODO Сделать редирект на список заявок клиента, когда он будет реализован
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
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