using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class RestrictionRepository : IRestrictionRepository
    {
        private CinemaDbContext cdb;
        public RestrictionRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(AgeRestriction model)
        {
            cdb.age_restriction.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(AgeRestriction model)
        {
            cdb.age_restriction.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<AgeRestriction> Get(int id)
        {
            return await cdb.age_restriction.FirstOrDefaultAsync(x => x.restriction_id == id);
        }

        public async Task<List<AgeRestriction>> GetAll()
        {
            return await cdb.age_restriction.ToListAsync();
        }

        public async Task<AgeRestriction> Update(AgeRestriction model)
        {
            cdb.age_restriction.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
