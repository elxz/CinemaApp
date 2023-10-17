using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface ISessionService
    {
        Task<IBaseResponse<IEnumerable<Session>>> GetSessions();

        Task<IBaseResponse<Session>> GetSession(int id);

        Task<IBaseResponse<bool>> DeleteSession(Session session);

        Task<IBaseResponse<Session>> CreateSession(Session session);

        Task<IBaseResponse<Session>> Edit(Session session);
    }
}
