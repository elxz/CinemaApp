using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IFilmService
    {
        Task<IBaseResponse<IEnumerable<Film>>> GetFilms();

        Task<IBaseResponse<Film>> GetFilm(int id);

        Task<IBaseResponse<bool>> DeleteFilm(Film film);

        Task<IBaseResponse<Film>> CreateFilm(Film film);

        Task<IBaseResponse<Film>> Edit(Film film);
    }
}
