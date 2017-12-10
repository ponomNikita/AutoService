using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.DAL.Models
{
    [Table("Role", Schema = "user")]
    public class Role : TEntity
    {
        public Role()
        {
            Users = new Collection<User>();
        }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}