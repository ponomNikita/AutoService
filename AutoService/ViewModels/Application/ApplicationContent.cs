using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoService.DAL.FilterModel;

namespace AutoService.ViewModels.Application
{
    public class ApplicationContent
    {
        public ApplicationFilter Filter { get; set; }
        public IEnumerable<AutoService.DAL.Models.Application> Items { get; set; }
    }
}