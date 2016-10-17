using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL.Models;

namespace AutoService.DAL.FilterModel
{
    public class ApplicationFilter
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CarModel { get; set; }
        public string CarNumber{ get; set; }
        public int? Status { get; set; }
        public int? RequestType { get; set; }
        public DateTime? Date { get; set; }
    }
}
