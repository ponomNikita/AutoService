using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Services.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name="Логин")]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Запомнить")]
        [Required]
        public bool RememberMe { get; set; }
    }
}