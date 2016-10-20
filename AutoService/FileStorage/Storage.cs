using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoService.DAL;
using System.Web.Configuration;

namespace AutoService.FileStorage
{
    public static class Storage
    {
        public static string GetImagePathById(int attachmentId)
        {
            IAutoServiceUnitOfWork uow = new AutoServiceUnitOfWork();

            var attachment = uow.Attachments.Get(attachmentId);
            var relativePath = attachment.RelativePath ?? string.Empty;
            var filePath = WebConfigurationManager.AppSettings["StoragePath"] + relativePath + attachment.Name;

            return filePath.Remove(0, 1);
        }
    }
}