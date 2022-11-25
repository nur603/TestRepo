using System.ComponentModel.DataAnnotations;

namespace Redemption.Models
{
    public class AuthVM
    {
        [Required(ErrorMessage = "Не верно указана почта")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]

        public string Password { get; set; }
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
