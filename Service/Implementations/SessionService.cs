using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Cinema.Repositories;
using Microsoft.AspNetCore.Http;

namespace Cinema.Service.Implementations
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository sessionRepository;
        public SessionService(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        async Task<IBaseResponse<IEnumerable<Session>>> ISessionService.GetSessions()
        {
            var baseResponse = new BaseResponse<IEnumerable<Session>>();
            try
            {
                var sessions = await sessionRepository.GetAll();
                if (sessions.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.SessionNotFound;
                    return baseResponse;
                }

                baseResponse.Data = sessions;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Session>>()
                {
                    Description = $"[GetSessions] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Session>> ISessionService.GetSession(int id)
        {
            var baseResponse = new BaseResponse<Session>();
            try
            {
                var session = await sessionRepository.Get(id);
                if (session == null)
                {
                    baseResponse.Description = "Фильм не найден";
                    baseResponse.StatusCode = Enum.StatusCode.SessionNotFound;
                    return baseResponse;
                }

                baseResponse.Data = session;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Session>()
                {
                    Description = $"[GetSession] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Session>> ISessionService.CreateSession(Session sessionModel)
        {
            var baseResponse = new BaseResponse<Session>();
            try
            {
                var session = new Session()
                {
                    fk_film = sessionModel.fk_film,
                    fk_hall = sessionModel.fk_hall,
                    session_date = sessionModel.session_date,
                    start_time = sessionModel.start_time,
                };

                await sessionRepository.Create(session);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Session>()
                {
                    Description = $"[CreateSession] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<bool>> ISessionService.DeleteSession(Session session)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (session == null)
                {
                    baseResponse.Description = "Сеанс не найден";
                    baseResponse.StatusCode = Enum.StatusCode.SessionNotFound;
                    return baseResponse;
                }

                await sessionRepository.Delete(session);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteSession] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Session>> Edit(Session model)
        {
            var baseResponse = new BaseResponse<Session>();
            try
            {
                var session = await sessionRepository.Get(model.session_id);
                if (session == null)
                {
                    baseResponse.StatusCode = Enum.StatusCode.SessionNotFound;
                    baseResponse.Description = "Сеанс не найден";
                    return baseResponse;
                }

                session.fk_film = model.fk_film;
                session.fk_hall = model.fk_hall;
                session.session_date = model.session_date;
                session.start_time = model.start_time;
                session.end_time = model.end_time;

                await sessionRepository.Update(session);
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Session>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}