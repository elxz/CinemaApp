using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IPricingService
    {
        Task<IBaseResponse<IEnumerable<Pricing>>> GetPricings();

        Task<IBaseResponse<Pricing>> GetPricing(int id);

        Task<IBaseResponse<Pricing>> GetPricingByH(int hall);
    }
}
