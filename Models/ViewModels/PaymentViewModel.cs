namespace Cinema.Models.ViewModels
{
    public class PaymentViewModel
    {
        public List<SessionRowPlace> sessionRowPlaces { get; set; }
        public int ticketsCount { get; set; }
        public Session session { get; set; }
        public Hall hall { get; set; }
        public Film film { get; set; }
        public double finale_price { get; set; }
        public bool isAvailable { get; set; } = true;
    }
}
