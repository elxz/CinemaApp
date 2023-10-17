using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class UserRepository : IUserRepository
    {
        private CinemaDbContext cdb;
        public UserRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(User model)
        {
            cdb.user_.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(User model)
        {
            cdb.user_.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<User> Get(int id)
        {
            return await cdb.user_.FirstOrDefaultAsync(x => x.user_id == id);
        }

        public async Task<User> GetByLogin(string login)
        {
            return await cdb.user_.FirstOrDefaultAsync(x => x.user_login == login);
        }

        public async Task<List<User>> GetAll()
        {
            return await cdb.user_.ToListAsync();
        }

        public async Task<User> Update(User model)
        {
            cdb.user_.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
