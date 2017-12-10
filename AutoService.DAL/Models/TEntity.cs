using System.ComponentModel.DataAnnotations;

namespace AutoService.DAL.Models
{
    public class TEntity
    {
        [Required]
        public int Id { get; set; }
    }
}