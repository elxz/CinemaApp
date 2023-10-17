using Cinema.Models;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IRestrictionService
    {
        Task<IBaseResponse<IEnumerable<AgeRestriction>>> GetRestrictions();
    }
}
