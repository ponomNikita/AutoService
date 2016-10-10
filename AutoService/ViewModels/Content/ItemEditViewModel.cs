using AutoService.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AutoService.ViewModels.Content
{
    public class ItemEditViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Название")]
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
        public HttpPostedFileBase[] Upload { get; set; }
    }
}