using System.ComponentModel.DataAnnotations;

namespace tparf.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Укажите вашу электронную почту")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }
        
    }
}
