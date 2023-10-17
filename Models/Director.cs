using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Director
    {
        [Key]
        [Display(Name = "Режиссер")]
        public int director_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Имя режиссера")]
        [MaxLength(20, ErrorMessage = "Максимальное кол-во символов - 50")]
        [Required]
        public string director_firstname { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Фамилия режиссера")]
        [MaxLength(20, ErrorMessage = "Максимальное кол-во символов - 50")]
        [Required]
        public string director_lastname { get; set; } = string.Empty;
    }
}
