using BreweryProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryProject.DataManagers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brewery>()
           .HasMany(p => p.Beers)
           .WithOne()
           .HasForeignKey(p => p.BeerId);
        }
    }
}
