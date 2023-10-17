using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IPricingRepository : IBaseRepository<Pricing>
    {
        Task<Pricing> GetByH(int hall);
    }
}
