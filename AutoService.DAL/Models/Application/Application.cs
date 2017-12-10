using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoService.DAL.Models
{
    [Table("Application", Schema = "app")]
    public class Application : TEntity
    {
        public Application()
        {
            CoordinationRequests = new Collection<CoordinationRequest>();
        }
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

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
        public virtual ICollection<CoordinationRequest> CoordinationRequests { get; set; }
    }
}
