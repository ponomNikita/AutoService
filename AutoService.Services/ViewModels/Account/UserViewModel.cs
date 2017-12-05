using System.ComponentModel.DataAnnotations;
using AutoService.DAL.Models;

namespace AutoService.Services.ViewModels
{
    public class UserViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Логин")]
        [MaxLength(50)]
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
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Display(Name = "Номер телефона")]
        [MaxLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Display(Name = "Адрес")]
        [MaxLength(50)]
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