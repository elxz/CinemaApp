using Cinema.Models;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IHallService
    {
        Task<IBaseResponse<IEnumerable<Hall>>> GetHalls();

        Task<IBaseResponse<Hall>> GetHall(int id);

        Task<IBaseResponse<bool>> DeleteHall(Hall hall);

        Task<IBaseResponse<Hall>> CreateHall(Hall hall);

        Task<IBaseResponse<Hall>> Edit(Hall hall);
    }
}
