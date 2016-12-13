using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AutoService.DAL.Models;
using AutoService.Services.Services;

namespace AutoService.Services.ViewModels
{
    public class UserViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Display(Name = "Номер телефона")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        public bool IsCreate { get; set; }

        public UserViewModel() { }

        public UserViewModel(User user)
        {
            id = user.id;
            Login = user.Login;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Address = user.Address;
        }

        public void Copy(User destination)
        {
            destination.FirstName = FirstName;
            destination.LastName = LastName;
            destination.Login = Login;
            destination.Email = Email;
            destination.Address = Address;
            destination.PhoneNumber = PhoneNumber;
            destination.Password = Password;
        }

        public string HandleActionName
        {
            get
            {
                return IsCreate 
                    ? "CreateUser"
                    : "Edit";
            }
        }
    }
}