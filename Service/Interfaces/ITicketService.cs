using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface ITicketService
    {
        Task<IBaseResponse<IEnumerable<Ticket>>> GetTickets();

        Task<IBaseResponse<Ticket>> GetTicket(int id);

        Task<IBaseResponse<bool>> DeleteTicket(Ticket ticket);

        Task<IBaseResponse<Ticket>> CreateTicket(Ticket ticket);
    }
}
