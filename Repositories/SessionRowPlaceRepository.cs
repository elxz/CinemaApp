using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class SessionRowPlaceRepository : ISessionRowPlaceRepository
    {
        private CinemaDbContext cdb;
        public SessionRowPlaceRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(SessionRowPlace model)
        {
            cdb.session_row_place.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(SessionRowPlace model)
        {
            cdb.session_row_place.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<SessionRowPlace> Get(int id)
        {
            return await cdb.session_row_place.FirstOrDefaultAsync(x => x.srp_id == id);
        }

        public async Task<List<SessionRowPlace>> GetAll()
        {
            return await cdb.session_row_place.ToListAsync();
        }

        public async Task<SessionRowPlace> Update(SessionRowPlace model)
        {
            cdb.session_row_place.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
