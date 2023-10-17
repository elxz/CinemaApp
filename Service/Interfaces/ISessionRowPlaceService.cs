using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface ISessionRowPlaceService
    {
        Task<IBaseResponse<IEnumerable<SessionRowPlace>>> GetSessionRowPlaces();

        Task<IBaseResponse<SessionRowPlace>> GetSessionRowPlace(int id);

        Task<IBaseResponse<bool>> DeleteSessionRowPlace(SessionRowPlace sessionRowPlace);

        Task<IBaseResponse<SessionRowPlace>> CreateSessionRowPlace(SessionRowPlace sessionRowPlace);

        Task<IBaseResponse<SessionRowPlace>> Edit(SessionRowPlace sessionRowPlace);
    }
}
