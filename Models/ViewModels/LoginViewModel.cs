using System.ComponentModel.DataAnnotations;

namespace Cinema.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string login { get; set; } = string.Empty;

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        public string password { get; set; } = string.Empty;
    }
}
