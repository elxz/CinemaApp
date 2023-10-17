using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class FilmGenreRepository : IFilmGenreRepository
    {
        private CinemaDbContext cdb;
        public FilmGenreRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(FilmGenre model)
        {
            cdb.film_genre.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(FilmGenre model)
        {
            cdb.film_genre.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<FilmGenre> Get(int id)
        {
            return await cdb.film_genre.FirstOrDefaultAsync(x => x.fg_id == id);
        }

        public async Task<List<FilmGenre>> GetAll()
        {
            return await cdb.film_genre.ToListAsync();
        }

        public async Task<FilmGenre> Update(FilmGenre model)
        {
            cdb.film_genre.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
