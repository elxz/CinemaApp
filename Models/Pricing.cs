using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Cinema.Models
{
    public class Pricing
    {
        [Key]
        public int pricing_id { get; set; }

        [Column(TypeName = "money")]
        public double price { get; set; }
        public int fk_hall { get; set; }
    }
}
