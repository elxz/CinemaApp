using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class FilmCountryRepository : IFilmCountryRepository
    {
        private CinemaDbContext cdb;
        public FilmCountryRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(FilmCountry model)
        {
            cdb.film_country.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(FilmCountry model)
        {
            cdb.film_country.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<FilmCountry> Get(int id)
        {
            return await cdb.film_country.FirstOrDefaultAsync(x => x.fc_id == id);
        }

        public async Task<List<FilmCountry>> GetAll()
        {
            return await cdb.film_country.ToListAsync();
        }

        public async Task<FilmCountry> Update(FilmCountry model)
        {
            cdb.film_country.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
