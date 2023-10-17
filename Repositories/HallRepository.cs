using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class HallRepository : IHallRepository
    {
        private CinemaDbContext cdb;
        public HallRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(Hall model)
        {
            cdb.hall.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Hall model)
        {
            cdb.hall.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<Hall> Get(int id)
        {
            return await cdb.hall.FirstOrDefaultAsync(x => x.hall_id == id);
        }

        public async Task<List<Hall>> GetAll()
        {
            return await cdb.hall.ToListAsync();
        }

        public async Task<Hall> Update(Hall model)
        {
            cdb.hall.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
