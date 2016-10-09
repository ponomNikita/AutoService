using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoService.DAL.Models;
using System.Web.Configuration;

namespace AutoService.Services
{
    public static class AttachmentService
    {
        public static string GetPath(Attachment attachment)
        {
            return (WebConfigurationManager.AppSettings["StoragePath"] + attachment.RelativePath + attachment.Name).Replace("~", string.Empty);
        }
    }
}