using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace AutoService.DAL.Models
{
    [Table("CoordinationResponse", Schema = "app")]
    public class CoordinationResponse : TEntity
    {
        public new int id { get; set; }

        public string Message { get; set; }

        public bool? IsAgree { get; set; }

    }
}