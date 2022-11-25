using System.ComponentModel.DataAnnotations;

namespace Redemption.Models
{
    public class UserRegVM
    {
        [Required]
        [RegularExpression(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+", ErrorMessage = "Укажите почту")]

        public string Email { get; set; }

        [Required]
        public string Name { get; set; }
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
