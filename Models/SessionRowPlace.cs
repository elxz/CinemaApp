using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class SessionRowPlace
    {
        [Key]
        public int srp_id { get; set; }

        public int fk_session { get; set; }

        public int row_ { get; set; }

        public int place_ { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string status { get; set; }

        public int fk_user { get; set; }
    }
}
