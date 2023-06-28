using System.ComponentModel.DataAnnotations;

namespace tparf.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Укажите вашу электронную почту")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }
    }
}
