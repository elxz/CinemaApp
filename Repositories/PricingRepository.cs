using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class PricingRepository : IPricingRepository
    {
        private CinemaDbContext cdb;
        public PricingRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(Pricing model)
        {
            cdb.pricing.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Pricing model)
        {
            cdb.pricing.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<Pricing> Get(int id)
        {
            return await cdb.pricing.FirstOrDefaultAsync(x => x.pricing_id == id);
        }

        public async Task<Pricing> GetByH(int hall)
        {
            return await cdb.pricing.FirstOrDefaultAsync(x => x.fk_hall == hall);
        }

        public async Task<List<Pricing>> GetAll()
        {
            return await cdb.pricing.ToListAsync();
        }

        public async Task<Pricing> Update(Pricing model)
        {
            cdb.pricing.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
