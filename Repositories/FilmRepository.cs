using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private CinemaDbContext cdb;
        public FilmRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(Film model)
        {
            cdb.film.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Film model)
        {
            cdb.film.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<Film> Get(int id)
        {
            return await cdb.film.FirstOrDefaultAsync(x => x.film_id == id);
        }

        public async Task<List<Film>> GetAll()
        {
            return await cdb.film.ToListAsync();
        }

        public async Task<Film> Update(Film model)
        {
            cdb.film.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
