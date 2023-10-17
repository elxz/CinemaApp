using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository directorRepository;
        public DirectorService(IDirectorRepository directorRepository)
        {
            this.directorRepository = directorRepository;
        }

        async Task<IBaseResponse<IEnumerable<Director>>> IDirectorService.GetDirectors()
        {
            var baseResponse = new BaseResponse<IEnumerable<Director>>();
            try
            {
                var directors = await directorRepository.GetAll();
                if (directors.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.DirectorNotFound;
                    return baseResponse;
                }

                baseResponse.Data = directors;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Director>>()
                {
                    Description = $"[GetDirectors] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Director>> IDirectorService.GetDirector(int id)
        {
            var baseResponse = new BaseResponse<Director>();
            try
            {
                var director = await directorRepository.Get(id);
                if (director == null)
                {
                    baseResponse.Description = "Фильм не найден";
                    baseResponse.StatusCode = Enum.StatusCode.DirectorNotFound;
                    return baseResponse;
                }

                baseResponse.Data = director;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Director>()
                {
                    Description = $"[GetDirector] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Director>> IDirectorService.CreateDirector(Director directorModel)
        {
            var baseResponse = new BaseResponse<Director>();
            try
            {
                var director = new Director()
                {
                    director_firstname = directorModel.director_firstname,
                    director_lastname = directorModel.director_lastname
                };

                await directorRepository.Create(director);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Director>()
                {
                    Description = $"[CreateDirector] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<bool>> IDirectorService.DeleteDirector(Director director)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (director == null)
                {
                    baseResponse.Description = "Фильм не найден";
                    baseResponse.StatusCode = Enum.StatusCode.DirectorNotFound;
                    return baseResponse;
                }

                await directorRepository.Delete(director);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteDirector] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}