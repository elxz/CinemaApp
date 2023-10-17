using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Session
    {
        [Key]
        [Display(Name = "Сеанс")]
        public int session_id { get; set; }

        [Required(ErrorMessage = "Введите фильм")]
        [Display(Name = "Фильм")]
        public int fk_film { get; set; }

        [Required(ErrorMessage = "Введите зал")]
        [Display(Name = "Зал")]
        public int fk_hall { get; set; }

        [BindProperty, DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Введите дату сеанса")]
        [Display(Name = "Дата сеанса")]
        public DateTime session_date { get; set; }

        [BindProperty, DataType(DataType.Time)]
        [Column(TypeName = "interval")]
        [Required(ErrorMessage = "Введите время начала")]
        [Display(Name = "Время начала")]
        public TimeSpan start_time { get; set; }

        [BindProperty, DataType(DataType.Time)]
        [Column(TypeName = "interval")]
        [Required(ErrorMessage = "Введите время окончания")]
        [Display(Name = "Время окончания")]
        public TimeSpan end_time { get; set; }
    }
}
