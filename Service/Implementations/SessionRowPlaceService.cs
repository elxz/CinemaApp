using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class SessionRowPlaceService : ISessionRowPlaceService
    {
        private readonly ISessionRowPlaceRepository sessionRowPlaceRepository;
        public SessionRowPlaceService(ISessionRowPlaceRepository sessionRowPlaceRepository)
        {
            this.sessionRowPlaceRepository = sessionRowPlaceRepository;
        }

        async Task<IBaseResponse<IEnumerable<SessionRowPlace>>> ISessionRowPlaceService.GetSessionRowPlaces()
        {
            var baseResponse = new BaseResponse<IEnumerable<SessionRowPlace>>();
            try
            {
                var sessionRowPlaces = await sessionRowPlaceRepository.GetAll();
                if (sessionRowPlaces.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.SessionRowPlaceNotFound;
                    return baseResponse;
                }

                baseResponse.Data = sessionRowPlaces;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<SessionRowPlace>>()
                {
                    Description = $"[GetSessionRowPlaces] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<SessionRowPlace>> ISessionRowPlaceService.GetSessionRowPlace(int id)
        {
            var baseResponse = new BaseResponse<SessionRowPlace>();
            try
            {
                var sessionRowPlace = await sessionRowPlaceRepository.Get(id);
                if (sessionRowPlace == null)
                {
                    baseResponse.Description = "srp не найден";
                    baseResponse.StatusCode = Enum.StatusCode.SessionRowPlaceNotFound;
                    return baseResponse;
                }

                baseResponse.Data = sessionRowPlace;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<SessionRowPlace>()
                {
                    Description = $"[GetSessionRowPlace] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<SessionRowPlace>> ISessionRowPlaceService.CreateSessionRowPlace(SessionRowPlace sessionRowPlaceModel)
        {
            var baseResponse = new BaseResponse<SessionRowPlace>();
            try
            {
                var sessionRowPlace = new SessionRowPlace()
                {
                    fk_session = sessionRowPlaceModel.fk_session,
                    place_ = sessionRowPlaceModel.place_,
                    row_ = sessionRowPlaceModel.row_,
                    status = sessionRowPlaceModel.status
                };

                await sessionRowPlaceRepository.Create(sessionRowPlace);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<SessionRowPlace>()
                {
                    Description = $"[CreateSessionRowPlace] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<bool>> ISessionRowPlaceService.DeleteSessionRowPlace(SessionRowPlace sessionRowPlace)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (sessionRowPlace == null)
                {
                    baseResponse.Description = "srp не найден";
                    baseResponse.StatusCode = Enum.StatusCode.SessionRowPlaceNotFound;
                    return baseResponse;
                }

                await sessionRowPlaceRepository.Delete(sessionRowPlace);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteSessionRowPlace] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<SessionRowPlace>> Edit(SessionRowPlace model)
        {
            var baseResponse = new BaseResponse<SessionRowPlace>();
            try
            {
                var sessionRowPlace = await sessionRowPlaceRepository.Get(model.srp_id);
                if (sessionRowPlace == null)
                {
                    baseResponse.StatusCode = Enum.StatusCode.SessionRowPlaceNotFound;
                    baseResponse.Description = "srp не найден";
                    return baseResponse;
                }

                sessionRowPlace.fk_session = model.fk_session;
                sessionRowPlace.row_ = model.row_;
                sessionRowPlace.place_ = model.place_;
                sessionRowPlace.status = model.status;

                await sessionRowPlaceRepository.Update(sessionRowPlace);
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<SessionRowPlace>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}