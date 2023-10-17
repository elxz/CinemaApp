using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class HallService : IHallService
    {
        private readonly IHallRepository hallRepository;
        public HallService(IHallRepository hallRepository)
        {
            this.hallRepository = hallRepository;
        }

        async Task<IBaseResponse<IEnumerable<Hall>>> IHallService.GetHalls()
        {
            var baseResponse = new BaseResponse<IEnumerable<Hall>>();
            try
            {
                var halls = await hallRepository.GetAll();
                if (halls.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.HallNotFound;
                    return baseResponse;
                }

                baseResponse.Data = halls;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Hall>>()
                {
                    Description = $"[GetHalls] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Hall>> IHallService.GetHall(int id)
        {
            var baseResponse = new BaseResponse<Hall>();
            try
            {
                var hall = await hallRepository.Get(id);
                if (hall == null)
                {
                    baseResponse.Description = "Зал не найден";
                    baseResponse.StatusCode = Enum.StatusCode.HallNotFound;
                    return baseResponse;
                }

                baseResponse.Data = hall;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Hall>()
                {
                    Description = $"[GetHall] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Hall>> IHallService.CreateHall(Hall hallModel)
        {
            var baseResponse = new BaseResponse<Hall>();
            try
            {
                var hall = new Hall()
                {
                    screen_type = hallModel.screen_type,
                    count_of_rows = hallModel.count_of_rows,
                    description = hallModel.description,
                    hall_number = hallModel.hall_number,
                    places_per_row = hallModel.places_per_row,
                    hall_image = hallModel.hall_image,
                };

                await hallRepository.Create(hall);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Hall>()
                {
                    Description = $"[CreateHall] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<bool>> IHallService.DeleteHall(Hall hall)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (hall == null)
                {
                    baseResponse.Description = "Зал не найден";
                    baseResponse.StatusCode = Enum.StatusCode.HallNotFound;
                    return baseResponse;
                }

                await hallRepository.Delete(hall);
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

        public async Task<IBaseResponse<Hall>> Edit(Hall model)
        {
            var baseResponse = new BaseResponse<Hall>();
            try
            {
                var hall = await hallRepository.Get(model.hall_id);
                if (hall == null)
                {
                    baseResponse.StatusCode = Enum.StatusCode.HallNotFound;
                    baseResponse.Description = "Зал не найден";
                    return baseResponse;
                }

                hall.description = model.description;
                hall.count_of_rows = model.count_of_rows;
                hall.hall_number = model.hall_number;
                hall.screen_type = model.screen_type;
                hall.places_per_row = model.places_per_row;
                hall.hall_image = model.hall_image;

                await hallRepository.Update(hall);
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Hall>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}