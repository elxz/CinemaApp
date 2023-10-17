using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private CinemaDbContext cdb;
        public DirectorRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(Director model)
        {
            cdb.director.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Director model)
        {
            cdb.director.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<Director> Get(int id)
        {
            return await cdb.director.FirstOrDefaultAsync(x => x.director_id == id);
        }

        public async Task<List<Director>> GetAll()
        {
            return await cdb.director.ToListAsync();
        }

        public async Task<Director> Update(Director model)
        {
            cdb.director.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
