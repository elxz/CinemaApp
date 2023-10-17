using Cinema.Models.ViewModels;
using Cinema.Models;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IGenreService
    {
        Task<IBaseResponse<IEnumerable<Genre>>> GetGenres();

        Task<IBaseResponse<Genre>> GetGenre(int id);

        Task<IBaseResponse<bool>> DeleteGenre(Genre genre);

        Task<IBaseResponse<Genre>> CreateGenre(Genre genre);

        Task<IBaseResponse<Genre>> Edit(Genre genre);
    }
}
