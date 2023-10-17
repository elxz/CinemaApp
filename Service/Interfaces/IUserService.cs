using Cinema.Models;
using Cinema.Models.ViewModels;
using Cinema.Response;

namespace Cinema.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<User>>> GetUsers();

        Task<IBaseResponse<User>> GetUser(int id);

        Task<IBaseResponse<User>> GetUserByLogin(string login);

        Task<IBaseResponse<bool>> DeleteUser(User user);

        Task<IBaseResponse<User>> CreateUser(User user);

        Task<IBaseResponse<User>> Edit(User user);
    }
}
