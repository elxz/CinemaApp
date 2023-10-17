using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class RestrictionService : IRestrictionService
    {
        private readonly IRestrictionRepository restrictionRepository;
        public RestrictionService(IRestrictionRepository restrictionRepository)
        {
            this.restrictionRepository = restrictionRepository;
        }

        async Task<IBaseResponse<IEnumerable<AgeRestriction>>> IRestrictionService.GetRestrictions()
        {
            var baseResponse = new BaseResponse<IEnumerable<AgeRestriction>>();
            try
            {
                var restrictions = await restrictionRepository.GetAll();
                if (restrictions.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.RestrictionNotFound;
                    return baseResponse;
                }

                baseResponse.Data = restrictions;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<AgeRestriction>>()
                {
                    Description = $"[GetRestrictions] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}