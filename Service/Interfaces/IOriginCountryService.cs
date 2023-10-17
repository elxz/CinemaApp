using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IOriginCountryService
    {
        Task<IBaseResponse<IEnumerable<OriginCountry>>> GetOriginCountries();

        Task<IBaseResponse<OriginCountry>> GetOriginCountry(int id);

        Task<IBaseResponse<bool>> DeleteOriginCountry(OriginCountry country);

        Task<IBaseResponse<OriginCountry>> CreateOriginCountry(OriginCountry country);

        Task<IBaseResponse<OriginCountry>> Edit(OriginCountry country);
    }
}
