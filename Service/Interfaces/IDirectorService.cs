using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IDirectorService
    {
        Task<IBaseResponse<IEnumerable<Director>>> GetDirectors();

        Task<IBaseResponse<Director>> GetDirector(int id);

        Task<IBaseResponse<bool>> DeleteDirector(Director director);

        Task<IBaseResponse<Director>> CreateDirector(Director director);
    }
}
