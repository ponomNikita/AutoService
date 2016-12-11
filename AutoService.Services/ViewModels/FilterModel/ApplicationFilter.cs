using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL.Models;
using System.ComponentModel.DataAnnotations;
using AutoService.Enums;
using AutoService.Services.Enums;

namespace AutoService.Services.ViewModels
{
    public class ApplicationFilter
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        [Display(Name = "Модель автомобиля")]
        public string CarModel { get; set; }
        [Display(Name = "Номер автомобиля")]
        public string CarNumber{ get; set; }
        public int? Status { get; set; }
        public int? RequestType { get; set; }
        public DateTime? Date { get; set; }

        public IDictionary<string, string> Statuses
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "", "" },
                    { ApplicationStatus.WaitForApprove.Description(), ((int)ApplicationStatus.WaitForApprove).ToString() },
                    { ApplicationStatus.WaitForReparing.Description(), ((int)ApplicationStatus.WaitForReparing).ToString() },
                    { ApplicationStatus.WaitForDiagnostic.Description(), ((int)ApplicationStatus.WaitForDiagnostic).ToString() },
                    { ApplicationStatus.WaitForSupply.Description(), ((int)ApplicationStatus.WaitForSupply).ToString() },
                    { ApplicationStatus.Reparing.Description(), ((int)ApplicationStatus.Reparing).ToString() },
                    { ApplicationStatus.Done.Description(), ((int)ApplicationStatus.Done).ToString() }
                };
            }
        }
    }
}
