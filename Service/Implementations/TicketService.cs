using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        async Task<IBaseResponse<IEnumerable<Ticket>>> ITicketService.GetTickets()
        {
            var baseResponse = new BaseResponse<IEnumerable<Ticket>>();
            try
            {
                var tickets = await ticketRepository.GetAll();
                if (tickets.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = tickets;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Ticket>>()
                {
                    Description = $"[GetTickets] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Ticket>> ITicketService.GetTicket(int id)
        {
            var baseResponse = new BaseResponse<Ticket>();
            try
            {
                var ticket = await ticketRepository.Get(id);
                if (ticket == null)
                {
                    baseResponse.Description = "Фильм не найден";
                    baseResponse.StatusCode = Enum.StatusCode.TicketNotFound;
                    return baseResponse;
                }

                baseResponse.Data = ticket;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Ticket>()
                {
                    Description = $"[GetTicket] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Ticket>> ITicketService.CreateTicket(Ticket ticketModel)
        {
            var baseResponse = new BaseResponse<Ticket>();
            try
            {
                var ticket = new Ticket()
                {
                    fk_price = ticketModel.fk_price,
                    fk_srp = ticketModel.fk_srp,
                    sale_date = ticketModel.sale_date,
                    fk_user = ticketModel.fk_user,
                };

                await ticketRepository.Create(ticket);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Ticket>()
                {
                    Description = $"[CreateTicket] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<bool>> ITicketService.DeleteTicket(Ticket ticket)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (ticket == null)
                {
                    baseResponse.Description = "Фильм не найден";
                    baseResponse.StatusCode = Enum.StatusCode.TicketNotFound;
                    return baseResponse;
                }

                await ticketRepository.Delete(ticket);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteTicket] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}