using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.DAL.Models
{
    [Table("Module", Schema = "settings")]
    public class Module : TEntity
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
