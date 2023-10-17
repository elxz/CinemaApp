using Cinema.Enum;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class User
    {
        [Key]
        [Display(Name = "ИД Пользователя")]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        [Column(TypeName = "varchar(50)")]
        [MaxLength(50, ErrorMessage = "Максимальное кол-во символов - 50")]
        public string user_firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите логин")]
        [Display(Name = "Фамилия")]
        [Column(TypeName = "varchar(50)")]
        [MaxLength(50, ErrorMessage = "Максимальное кол-во символов - 50")]
        public string user_lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите логин")]
        [Column(TypeName = "varchar(20)")]
        [Display(Name = "Логин")]
        [MaxLength(20, ErrorMessage = "Максимальное кол-во символов - 20")]
        [MinLength(4, ErrorMessage = "Минимальное кол-во символов - 4")]
        public string user_login { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        [Column(TypeName = "varchar(64)")]
        [Display(Name = "Пароль")]
        [MinLength(4, ErrorMessage = "Минимальное кол-во символов - 4")]
        public string user_password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите номер телефона")]
        [Column(TypeName = "varchar(12)")]
        [Display(Name = "Номер телефона")]
        [MinLength(12, ErrorMessage = "Укажите правильный номер телефона")]
        [MaxLength(12, ErrorMessage = "Укажите правильный номер телефона")]
        public string user_phone_number { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите дату рождения")]
        [BindProperty, DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [Display(Name = "Дата рождения")]
        public DateTime user_date_of_birth { get; set; }

        [Display(Name = "Роль")]
        [Column(TypeName = "varchar(10)")]
        public string role_ { get; set; } = string.Empty;
    }
}
