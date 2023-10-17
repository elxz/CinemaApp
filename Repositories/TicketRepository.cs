using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private CinemaDbContext cdb;
        public TicketRepository(CinemaDbContext cdb)
        {
            this.cdb = cdb;
        }

        public async Task<bool> Create(Ticket model)
        {
            cdb.ticket.Add(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Ticket model)
        {
            cdb.ticket.Remove(model);
            await cdb.SaveChangesAsync();

            return true;
        }

        public async Task<Ticket> Get(int id)
        {
            return await cdb.ticket.FirstOrDefaultAsync(x => x.ticket_id == id);
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await cdb.ticket.ToListAsync();
        }

        public async Task<Ticket> Update(Ticket model)
        {
            cdb.ticket.Update(model);
            await cdb.SaveChangesAsync();

            return model;
        }
    }
}
