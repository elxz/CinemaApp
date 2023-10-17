using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IFilmGenreService
    {
        Task<IBaseResponse<IEnumerable<FilmGenre>>> GetFilmGenres();
        Task<IBaseResponse<FilmGenre>> CreateFilmGenre(FilmGenre filmGenre);
    }
}
