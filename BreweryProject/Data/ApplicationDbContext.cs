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
            modelBuilder.Entity<Brewery>(b =>
            {
                b.HasKey(e => e.BreweryId);
                b.Property(e => e.BreweryId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Beer>(b =>
            {
                b.HasKey(e => e.BeerId);
                b.Property(e => e.BeerId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Brewery>()
           .HasMany(p => p.Beers)
           .WithOne()
           .HasForeignKey(p => p.BreweryId);

           
            modelBuilder.Entity<Brewery>().HasIndex(_ => _.Name).IsUnique();

            modelBuilder.Entity<Beer>().HasIndex(_ => _.Name).IsUnique();
        }
    }
}
