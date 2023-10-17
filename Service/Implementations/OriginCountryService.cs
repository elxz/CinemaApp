using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;

namespace Cinema.Service.Implementations
{
    public class OriginCountryService : IOriginCountryService
    {
        private readonly IOriginCountryRepository countryRepository;
        public OriginCountryService(IOriginCountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        async Task<IBaseResponse<IEnumerable<OriginCountry>>> IOriginCountryService.GetOriginCountries()
        {
            var baseResponse = new BaseResponse<IEnumerable<OriginCountry>>();
            try
            {
                var countries = await countryRepository.GetAll();
                if (countries.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = countries;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OriginCountry>>()
                {
                    Description = $"[GetOriginCountries] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<OriginCountry>> IOriginCountryService.GetOriginCountry(int id)
        {
            var baseResponse = new BaseResponse<OriginCountry>();
            try
            {
                var country = await countryRepository.Get(id);
                if (country == null)
                {
                    baseResponse.Description = "Страна не найдена";
                    baseResponse.StatusCode = Enum.StatusCode.CountryNotFound;
                    return baseResponse;
                }

                baseResponse.Data = country;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<OriginCountry>()
                {
                    Description = $"[GetOriginCountry] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<OriginCountry>> IOriginCountryService.CreateOriginCountry(OriginCountry countryModel)
        {
            var baseResponse = new BaseResponse<OriginCountry>();
            try
            {
                var country = new OriginCountry()
                {
                    country_name = countryModel.country_name
                };

                await countryRepository.Create(country);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<OriginCountry>()
                {
                    Description = $"[CreateOriginCountry] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<bool>> IOriginCountryService.DeleteOriginCountry(OriginCountry country)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                if (country == null)
                {
                    baseResponse.Description = "Страна не найдена";
                    baseResponse.StatusCode = Enum.StatusCode.CountryNotFound;
                    return baseResponse;
                }

                await countryRepository.Delete(country);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteOriginCountry] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<OriginCountry>> Edit(OriginCountry model)
        {
            var baseResponse = new BaseResponse<OriginCountry>();
            try
            {
                var country = await countryRepository.Get(model.country_id);
                if (country == null)
                {
                    baseResponse.StatusCode = Enum.StatusCode.CountryNotFound;
                    baseResponse.Description = "Страна не найдена";
                    return baseResponse;
                }

                country.country_name = model.country_name;

                await countryRepository.Update(country);
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<OriginCountry>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}