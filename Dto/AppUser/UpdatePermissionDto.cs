using System.ComponentModel.DataAnnotations;

namespace tparf.Dto
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "Укажите вашу электронную почту")]
        public string Email { get; set; }

    }
}
