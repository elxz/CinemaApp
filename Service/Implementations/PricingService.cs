using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class PricingService : IPricingService
    {
        private readonly IPricingRepository pricingRepository;
        public PricingService(IPricingRepository pricingRepository)
        {
            this.pricingRepository = pricingRepository;
        }

        async Task<IBaseResponse<IEnumerable<Pricing>>> IPricingService.GetPricings()
        {
            var baseResponse = new BaseResponse<IEnumerable<Pricing>>();
            try
            {
                var prices = await pricingRepository.GetAll();
                if (prices.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.PricingNotFound;
                    return baseResponse;
                }

                baseResponse.Data = prices;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Pricing>>()
                {
                    Description = $"[GetPricings] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Pricing>> IPricingService.GetPricing(int id)
        {
            var baseResponse = new BaseResponse<Pricing>();
            try
            {
                var pricing = await pricingRepository.Get(id);
                if (pricing == null)
                {
                    baseResponse.Description = "Ценник не найден";
                    baseResponse.StatusCode = Enum.StatusCode.PricingNotFound;
                    return baseResponse;
                }

                baseResponse.Data = pricing;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Pricing>()
                {
                    Description = $"[GetPricing] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<Pricing>> IPricingService.GetPricingByH(int hall)
        {
            var baseResponse = new BaseResponse<Pricing>();
            try
            {
                var pricing = await pricingRepository.GetByH(hall);
                if (pricing == null)
                {
                    baseResponse.Description = "Ценник не найден";
                    baseResponse.StatusCode = Enum.StatusCode.PricingNotFound;
                    return baseResponse;
                }

                baseResponse.Data = pricing;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Pricing>()
                {
                    Description = $"[GetPricing] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}