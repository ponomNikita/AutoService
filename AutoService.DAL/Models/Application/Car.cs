using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoService.DAL.Models
{
    public class Car : TEntity
    {
        [Required]
        [MaxLength(50)]
        [DisplayName("Модель автомобиля")]
        public string Model { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Регистрационный номер")]
        public string RegNumber { get; set; }

        [DisplayName("Год выпуска")]
        public DateTime? Year { get; set; }
    }
}