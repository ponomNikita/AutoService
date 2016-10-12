using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.DAL.Models
{
    [Table("User_Role", Schema = "user")]
    public class User_Role : TEntity
    {
        public new int id { get; set; }
        public int userId { get; set; }
        public int roleId { get; set; }
    }
}