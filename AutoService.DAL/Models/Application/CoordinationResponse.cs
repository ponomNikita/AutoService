using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.DAL.Models
{
    [Table("CoordinationResponse", Schema = "app")]
    public class CoordinationResponse : TEntity
    {
        public string Message { get; set; }

        public bool? IsAgree { get; set; }

    }
}