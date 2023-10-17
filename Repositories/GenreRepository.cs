using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private CinemaDbContext cdb;
        public GenreRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(Genre model)
        {
            cdb.genre.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Genre model)
        {
            cdb.genre.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<Genre> Get(int id)
        {
            return await cdb.genre.FirstOrDefaultAsync(x => x.genre_id == id);
        }

        public async Task<List<Genre>> GetAll()
        {
            return await cdb.genre.ToListAsync();
        }

        public async Task<Genre> Update(Genre model)
        {
            cdb.genre.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
