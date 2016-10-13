using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoService.DAL;
using AutoService.DAL.Models;
using AutoService.Enums;
using AutoService.Logger;

namespace SinglePageSite.Controllers
{
    public class ApplicationController : Controller
    {
        private ILogger Logger;
        private UnitOfWork uow;

        public ApplicationController()
        {
            Logger = new Logger();
            uow = new UnitOfWork();
        }
        [HttpGet]
        public ActionResult Create(int requestType = 0)
        {
            Application newApplication = new Application()
            {
                RequestType = requestType,
                Status = (int)ApplicationStatus.WaitForApprove,
                IsApproved = false
            };
            return View();
        }

        [HttpPost]
        public ActionResult Create(Application model)
        {
            if (!ModelState.IsValid)
                return View(model);
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = User.Identity.Name;

            Logger.Info("Запись в бд новой заявки...");
            uow.Applications.Create(model);
            uow.Save();
            Logger.Info("Успешно!");

            //TODO Сделать редирект на список заявок клиента, когда он будет реализован
            return RedirectToAction("Index", "Home");
        }
    }
}