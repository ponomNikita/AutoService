using AutoService.DAL;
using AutoService.DAL.Models;
using AutoService.Services.ViewModels;
using System;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Web.Services;
using AutoService.Logger;

namespace AutoService.Controllers
{
    public class ContentController : Controller
    {
        UnitOfWork uow;
        ILogger Logger;

        public ContentController()
        {
            uow = new UnitOfWork();
            Logger = new Logger.Logger();
        }
        [HttpGet]
        public ActionResult Index()
        {
            ContentListViewModel model = new ContentListViewModel();
            model.Items = GetListContent();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ItemEditViewModel model = new ItemEditViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ItemEditViewModel model)
        {
            Logger.Info("Попытка создания нового элемента в контенте...");

            Item newItem = new Item();

            newItem.Name = model.Name;
            newItem.Price = model.Price;
            newItem.CreatedDateTime = DateTime.Now;
            newItem.CreatedBy = User.Identity.Name;
            newItem.Description = model.Description;
            newItem.isActive = model.isActive;
            newItem.PreviewAttachmentId = 1; // TODO: Change 1 to selected preview attachment

            Logger.Info(string.Format("Попытка сохранения нового элемента контента в бд..."));
            uow.Items.Create(newItem);
            uow.Save();
            Logger.Info(string.Format("Успешно!"));

            var createdItem = uow.Items.GetAll().OrderByDescending(t => t.CreatedDateTime).FirstOrDefault();

            foreach (var upload in model.Upload)
            {
                if (upload != null)
                {
                    string fileName = Guid.NewGuid().ToString() +  System.IO.Path.GetExtension(upload.FileName);
                    try
                    {
                        Logger.Info(string.Format("Попытка сохранения изображения на сервер..."));
                        upload.SaveAs(Server.MapPath(WebConfigurationManager.AppSettings["StoragePath"] + fileName));
                        Logger.Info(string.Format("Успешно!"));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Ошибка при сохранении изображения на сервер", ex);
                        return View("_Exceprtion", new _Exception { ex = ex });
                    }

                    Attachment attachment = new Attachment();
                    attachment.Name = fileName;
                    attachment.RelativePath = "";
                    attachment.itemId = createdItem.id;
                    attachment.CreatedBy = User.Identity.Name;
                    attachment.CreatedDate = DateTime.Now;

                    Logger.Info(string.Format("Попытка создания аттачмента для элемента с id={0}", createdItem.id));
                    uow.Attachments.Create(attachment);
                    uow.Save();
                    Logger.Info("Успешно!");
                }
            }
            return Redirect("/Content/Index");
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var item = uow.Items.Include(t => t.Attachments).FirstOrDefault(o => o.id == id);
            return View("~/Views/Content/Detail.cshtml", item);
        }

        private List<Item> GetListContent()
        {
            return uow.Items.Include(t => t.Attachments).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            uow.Dispose();
            base.Dispose(disposing);
        }
    }
}