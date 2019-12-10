using FriendsCountry.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FriendsCountry.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Friend> Friends { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
