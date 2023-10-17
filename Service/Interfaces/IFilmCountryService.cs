using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IFilmCountryService
    {
        Task<IBaseResponse<IEnumerable<FilmCountry>>> GetFilmCountries();
        Task<IBaseResponse<FilmCountry>> CreateFilmCountry(FilmCountry filmCountry);
    }
}
