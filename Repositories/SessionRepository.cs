using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private CinemaDbContext cdb;
        public SessionRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(Session model)
        {
            cdb.session_.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Session model)
        {
            cdb.session_.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<Session> Get(int id)
        {
            return await cdb.session_.FirstOrDefaultAsync(x => x.session_id == id);
        }

        public async Task<List<Session>> GetAll()
        {
            return await cdb.session_.ToListAsync();
        }

        public async Task<Session> Update(Session model)
        {
            cdb.session_.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
