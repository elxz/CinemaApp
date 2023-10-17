using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        async Task<IBaseResponse<IEnumerable<Genre>>> IGenreService.GetGenres()
        {
            var baseResponse = new BaseResponse<IEnumerable<Genre>>();
            try
            {
                var genres = await genreRepository.GetAll();
                if (genres.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = genres;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Genre>>()
                {
                    Description = $"[GetGenres] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Genre>> IGenreService.GetGenre(int id)
        {
            var baseResponse = new BaseResponse<Genre>();
            try
            {
                var genre = await genreRepository.Get(id);
                if (genre == null)
                {
                    baseResponse.Description = "Жанр не найден";
                    baseResponse.StatusCode = Enum.StatusCode.GenreNotFound;
                    return baseResponse;
                }

                baseResponse.Data = genre;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Genre>()
                {
                    Description = $"[GetGenre] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Genre>> IGenreService.CreateGenre(Genre genreModel)
        {
            var baseResponse = new BaseResponse<Genre>();
            try
            {
                var genre = new Genre()
                {
                    genre_name = genreModel.genre_name
                };

                await genreRepository.Create(genre);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Genre>()
                {
                    Description = $"[CreateGenre] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<bool>> IGenreService.DeleteGenre(Genre genre)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (genre == null)
                {
                    baseResponse.Description = "Жанр не найден";
                    baseResponse.StatusCode = Enum.StatusCode.GenreNotFound;
                    return baseResponse;
                }

                await genreRepository.Delete(genre);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteGenre] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Genre>> Edit(Genre model)
        {
            var baseResponse = new BaseResponse<Genre>();
            try
            {
                var genre = await genreRepository.Get(model.genre_id);
                if (genre == null)
                {
                    baseResponse.StatusCode = Enum.StatusCode.GenreNotFound;
                    baseResponse.Description = "Фильм не найден";
                    return baseResponse;
                }

                genre.genre_name = model.genre_name;

                await genreRepository.Update(genre);
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Genre>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}