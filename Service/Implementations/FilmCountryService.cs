using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class FilmCountryService : IFilmCountryService
    {
        private readonly IFilmCountryRepository filmCountryRepository;
        public FilmCountryService(IFilmCountryRepository filmCountryRepository)
        {
            this.filmCountryRepository = filmCountryRepository;
        }

        async Task<IBaseResponse<IEnumerable<FilmCountry>>> IFilmCountryService.GetFilmCountries()
        {
            var baseResponse = new BaseResponse<IEnumerable<FilmCountry>>();
            try
            {
                var filmCountries = await filmCountryRepository.GetAll();
                if (filmCountries.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.FilmCountryNotFound;
                    return baseResponse;
                }

                baseResponse.Data = filmCountries;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<FilmCountry>>()
                {
                    Description = $"[GetFilmCountries] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<FilmCountry>> IFilmCountryService.CreateFilmCountry(FilmCountry filmCountryModel)
        {
            var baseResponse = new BaseResponse<FilmCountry>();
            try
            {
                var filmCountry = new FilmCountry()
                {
                    fk_film = filmCountryModel.fk_film,
                    fk_country = filmCountryModel.fk_country,
                };

                await filmCountryRepository.Create(filmCountry);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<FilmCountry>()
                {
                    Description = $"[CreateFilmGenre] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}