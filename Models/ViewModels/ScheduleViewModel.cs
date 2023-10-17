using Cinema.Models.Queries;

namespace Cinema.Models.ViewModels
{
    public class ScheduleViewModel
    {
        public List<Session> sessions { get; set; }
        public ScheduleQuery schedule { get; set; }
        public  List<Hall> halls { get; set; }
    }
}
