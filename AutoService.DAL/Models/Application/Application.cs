using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoService.DAL.Models
{
    [Table("Application", Schema = "app")]
    public class Application : TEntity
    {
        [Required]
        public new int id { get; set; }
        [Required]
        [Display(Name = "Модель автомобиля")]
        public string CarModel { get; set; }
        [Required]
        [Display(Name = "Номер автомобиля")]
        public string CarNumber { get; set; }
        [Required]
        [Display(Name = "Тип заявки")]
        public int RequestType { get; set; }
        [Display(Name = "Статус заявки")]
        public int? Status { get; set; }
        [Required]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Дополнительно")]
        public string Note { get; set; }
        public bool IsApproved { get; set; }
    }
}
