using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class AgeRestriction
    {
        [Key]
        [Display(Name = "Возрастное ограничение")]
        public int restriction_id { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string restriction { get; set; } = string.Empty;
    }
}
