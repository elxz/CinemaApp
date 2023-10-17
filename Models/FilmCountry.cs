using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class FilmCountry
    {
        [Key]
        public int fc_id { get; set; }

        public int fk_film { get; set; } = new();

        public int fk_country { get; set; } = new();
    }
}
