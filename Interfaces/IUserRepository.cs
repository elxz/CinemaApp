using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByLogin(string login);

    }
}
