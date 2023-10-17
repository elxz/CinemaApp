namespace Cinema.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T model);

        Task<T> Get(int id);

        Task<List<T>> GetAll();

        Task<bool> Delete(T model);

        Task<T> Update(T model);
    }
}
