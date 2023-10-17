using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Hall
    {
        [Key]
        [Display(Name = "Зал")]
        public int hall_id { get; set; }

        public int hall_number { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string screen_type { get; set; } = string.Empty;

        public int count_of_rows { get; set; }

        public int places_per_row { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public string description { get; set; } = string.Empty;

        [Column(TypeName = "varchar(500)")]
        public string hall_image { get; set; }
    }
}
