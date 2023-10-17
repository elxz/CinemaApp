using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class FilmGenre
    {
        [Key]
        public int fg_id { get; set; }
        public int fk_film { get; set; } = new();
        public int fk_genre { get; set; } = new();
    }
}
