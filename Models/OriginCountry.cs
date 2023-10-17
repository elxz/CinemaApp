using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class OriginCountry
    {
        [Key]
        [Display(Name = "Страна")]
        public int country_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string country_name { get; set; } = string.Empty;
    }
}
