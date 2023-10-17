using System;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cinema.Context
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> user_ { get; set; }
        public DbSet<AgeRestriction> age_restriction { get; set; }
        public DbSet<CustomerTicket> customer_ticket { get; set; }
        public DbSet<Director> director { get; set; }
        public DbSet<Film> film { get; set; }
        public DbSet<FilmCountry> film_country { get; set; }
        public DbSet<FilmGenre> film_genre { get; set; }
        public DbSet<Genre> genre { get; set; }
        public DbSet<Hall> hall { get; set; }
        public DbSet<OriginCountry> origin_country { get; set; }
        public DbSet<Pricing> pricing { get; set; }
        public DbSet<Session> session_ { get; set; }
        public DbSet<SessionRowPlace> session_row_place { get; set; }
        public DbSet<Ticket> ticket { get; set; }
    }
}
