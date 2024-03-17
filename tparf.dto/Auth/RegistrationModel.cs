using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace tparf.dto.Auth
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Заполните поле Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Заполните поле Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Заполните поле Название организации")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Заполните поле Номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Заполните поле Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните поле Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
