using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class FilmGenreService : IFilmGenreService
    {
        private readonly IFilmGenreRepository filmGenreRepository;
        public FilmGenreService(IFilmGenreRepository filmGenreRepository)
        {
            this.filmGenreRepository = filmGenreRepository;
        }

        async Task<IBaseResponse<IEnumerable<FilmGenre>>> IFilmGenreService.GetFilmGenres()
        {
            var baseResponse = new BaseResponse<IEnumerable<FilmGenre>>();
            try
            {
                var filmGenres = await filmGenreRepository.GetAll();
                if (filmGenres.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.FilmGenreNotFound;
                    return baseResponse;
                }

                baseResponse.Data = filmGenres;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<FilmGenre>>()
                {
                    Description = $"[GetFilmGenres] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<FilmGenre>> IFilmGenreService.CreateFilmGenre(FilmGenre filmGenreModel)
        {
            var baseResponse = new BaseResponse<FilmGenre>();
            try
            {
                var filmGenre = new FilmGenre()
                {
                    fk_film = filmGenreModel.fk_film,
                    fk_genre = filmGenreModel.fk_genre,
                };

                await filmGenreRepository.Create(filmGenre);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<FilmGenre>()
                {
                    Description = $"[CreateFilmGenre] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}