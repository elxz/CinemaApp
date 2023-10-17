using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class OriginCountryRepository : IOriginCountryRepository
    {
        private CinemaDbContext cdb;
        public OriginCountryRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(OriginCountry model)
        {
            cdb.origin_country.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(OriginCountry model)
        {
            cdb.origin_country.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<OriginCountry> Get(int id)
        {
            return await cdb.origin_country.FirstOrDefaultAsync(x => x.country_id == id);
        }

        public async Task<List<OriginCountry>> GetAll()
        {
            return await cdb.origin_country.ToListAsync();
        }

        public async Task<OriginCountry> Update(OriginCountry model)
        {
            cdb.origin_country.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
