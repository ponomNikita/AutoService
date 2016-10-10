using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoService.DAL.Models
{
    [Table("Item", Schema = "content")]
    public class Item : TEntity
    {
        public new int id { get; set; }
        [Required]
        [Display(Name="Название")]
        public string Name { get; set; }
        [Display(Name = "Цена")]
        public int Price { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public int PreviewAttachmentId { get; set; }
        [Display(Name = "Элемент активен")]
        public bool isActive { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}