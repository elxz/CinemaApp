using Cinema.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Npgsql.Internal.TypeHandlers.DateTimeHandlers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Cinema.Models
{
    public class Film
    {
        [Key]
        [Display(Name = "Фильм")]
        public int film_id { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Название фильма")]
        [MaxLength(150, ErrorMessage = "Максимальное кол-во символов - 150")]
        [Required(ErrorMessage = "Введите название фильма")]
        public string film_name { get; set; } = string.Empty;

        [Column(TypeName = "varchar(1000)")]
        [Display(Name = "Описание")]
        [MaxLength(150, ErrorMessage = "Максимальное кол-во символов - 1000")]
        [Required(ErrorMessage = "Введите описание")]
        public string description { get; set; } = string.Empty;

        [BindProperty, DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [Display(Name = "Дата выхода")]
        [Required(ErrorMessage = "Введите дату выхода")]
        public DateTime release_date { get; set; }

        [BindProperty, DataType(DataType.Time)]
        [Column(TypeName = "interval")]
        [Display(Name = "Длительность")]
        [Required(ErrorMessage = "Введите длительность")]
        public TimeSpan duration { get; set; }

        [Column(TypeName = "real")]
        [Display(Name = "Оценка")]
        [Required(ErrorMessage = "Введите оценку")]
        public float rating { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Бюджет")]
        [Required(ErrorMessage = "Введите бюджет")]
        public double budget { get; set; }

        [Display(Name = "Постер")]
        [Required(ErrorMessage = "Выберите изображение")]
        public string poster { get; set; } = string.Empty;

        [Display(Name = "Режисер")]
        [Required(ErrorMessage = "Введите режисера")]
        public int fk_director { get; set; }

        [Display(Name = "Возрастное ограничение")]
        [Required(ErrorMessage = "Введите возрастное ограничение")]
        public int fk_restriction { get; set; }
    }
}
