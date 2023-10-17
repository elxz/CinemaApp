using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Genre
    {
        [Key]
        [Display(Name = "Жанр")]
        public int genre_id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string genre_name { get; set; } = string.Empty;
    }
}
