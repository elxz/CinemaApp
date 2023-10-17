using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Ticket
    {
        [Key]
        public int ticket_id { get; set; }

        public int fk_srp { get; set; }

        [Column(TypeName = "date")]
        public DateTime sale_date { get; set; }

        public int fk_price { get; set; }

        public int fk_user { get; set; }
    }
}
