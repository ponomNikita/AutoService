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
        private IPermissionService permissionService;

        public ApplicationController()
            :base()
        {
            timeProvider = ServicesFactory.CreateDateTimeProvider();
            appService = ServicesFactory.CreateApplicationService(new Repository<Application>(DBContext.GetDBContext()), timeProvider, currentUser);
            permissionService = ServicesFactory.CreatePermissionService();
        }

        [HttpGet]
        [AuthorizeUser]
        public ActionResult Create(int requestType = 0)
        {
            if (permissionService.HasRole((int) Roles.Admin, currentUser.Login))
            {
                throw new AccessViolationException("Польтователь с ролью администратор не может создавать заявки");
            }

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

        [HttpGet]
        [AuthorizeUser(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            appService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AuthorizeUser]
        public ActionResult Edit(int id)
        {
            ApplicationEdit application = new ApplicationEdit(appService.GetById(id));

            return View("EditMain", application);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationEdit model)
        {
            if (!ModelState.IsValid)
                return View("EditMain", model);

            var modelError = appService.Edit(model);
            if (!string.IsNullOrWhiteSpace(modelError))
            {
                ModelState.AddModelError("", modelError);
                return View("EditMain", model);
            }

            return View("EditMain", model);
        }

        [HttpGet]
        [AuthorizeUser(Roles = "admin")]
        public ActionResult Approve(int id)
        {
            appService.Approve(id);

            return RedirectToAction("Index");
        }

        public IEnumerable<Application> GetFiltredContent(ApplicationFilter filter)
        {
            return appService.GetFiltered(filter);
        }
    }
}