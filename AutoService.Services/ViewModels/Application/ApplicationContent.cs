using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoService.Services.ViewModels
{
    public class ApplicationContent
    {
        public ApplicationFilter Filter { get; set; }
        public IEnumerable<AutoService.DAL.Models.Application> Items { get; set; }
    }
}