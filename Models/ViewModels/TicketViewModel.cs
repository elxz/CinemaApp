using Cinema.Models.Queries;

namespace Cinema.Models.ViewModels
{
    public class TicketViewModel
    {
        public Hall hall { get; set; }
        public Session session { get; set; }
        public List<SessionRowPlace> sessionRowPlaces { get; set; }
        public double pricing { get; set; }
    }
}
