using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class CustomerTicket
    {
        [Key]
        public int ct_id { get; set; }

        public int fk_user { get; set; }

        public int fk_ticket { get; set; }

    }
}
