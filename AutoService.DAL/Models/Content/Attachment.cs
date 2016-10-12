using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.DAL.Models
{
    [Table("Attachment", Schema = "content")]
    public class Attachment : TEntity
    {
        public new int id { get; set; }
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int? Code { get; set; }
        public bool isDeleted { get; set; }
        public int? itemId { get; set; }
        public Item Item { get; set; }
    }
}