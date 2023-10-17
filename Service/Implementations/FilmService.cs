using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository filmRepository;
        public FilmService(IFilmRepository filmRepository)
        {
            this.filmRepository = filmRepository;
        }

        async Task<IBaseResponse<IEnumerable<Film>>> IFilmService.GetFilms()
        {
            var baseResponse = new BaseResponse<IEnumerable<Film>>();
            try
            {
                var films = await filmRepository.GetAll();
                if(films.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.FilmNotFound;
                    return baseResponse;
                }

                baseResponse.Data = films;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Film>>()
                {
                    Description = $"[GetFilms] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Film>> IFilmService.GetFilm(int id)
        {
            var baseResponse = new BaseResponse<Film>();
            try
            {
                var film = await filmRepository.Get(id);
                if(film == null)
                {
                    baseResponse.Description = "Фильм не найден";
                    baseResponse.StatusCode = Enum.StatusCode.FilmNotFound;
                    return baseResponse;
                }

                baseResponse.Data = film;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[GetFilm] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Film>> IFilmService.CreateFilm(Film filmModel)
        {
            var baseResponse = new BaseResponse<Film>();
            try
            {
                var film = new Film()
                {
                    film_name = filmModel.film_name,
                    description = filmModel.description,
                    release_date = filmModel.release_date,
                    duration = filmModel.duration,
                    rating = filmModel.rating,
                    budget = filmModel.budget,
                    poster = filmModel.poster,
                    fk_director = filmModel.fk_director,
                    fk_restriction = filmModel.fk_restriction,
                };

                await filmRepository.Create(film);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[CreateFilm] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<bool>> IFilmService.DeleteFilm(Film film)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (film == null)
                {
                    baseResponse.Description = "Фильм не найден";
                    baseResponse.StatusCode = Enum.StatusCode.FilmNotFound;
                    return baseResponse;
                }

                await filmRepository.Delete(film);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteFilm] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Film>> Edit(Film model)
        {
            var baseResponse = new BaseResponse<Film>();
            try
            {
                var film =  await filmRepository.Get(model.film_id);
                if(film == null)
                {
                    baseResponse.StatusCode = Enum.StatusCode.FilmNotFound;
                    baseResponse.Description = "Фильм не найден";
                    return baseResponse;
                }

                film.film_name = model.film_name;
                film.description = model.description;
                film.budget = model.budget;
                film.fk_director = model.fk_director;
                film.duration = model.duration;
                film.fk_restriction = model.fk_restriction;
                film.release_date = model.release_date;
                film.poster = model.poster;
                film.rating = model.rating;

                await filmRepository.Update(film);
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}