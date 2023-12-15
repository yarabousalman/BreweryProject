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
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Beer>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Wholesaler>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SaleOrder>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Stock>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Brewery>()
           .HasMany(p => p.Beers)
           .WithOne()
           .HasForeignKey(p => p.BreweryId);

            modelBuilder.Entity<Wholesaler>()
           .HasMany(p => p.SaleOrders)
           .WithOne()
           .HasForeignKey(p => p.WholesalerId);

            modelBuilder.Entity<Wholesaler>()
            .HasMany(p => p.Stocks)
            .WithOne()
            .HasForeignKey(p => p.WholesalerId);

            modelBuilder.Entity<Beer>()
            .HasMany(p => p.SaleOrders)
            .WithOne()
            .HasForeignKey(p => p.BeerId);

            modelBuilder.Entity<Beer>()
            .HasMany(p => p.Stocks)
            .WithOne()
            .HasForeignKey(p => p.BeerId);


            modelBuilder.Entity<Beer>().HasIndex(_ => _.Name).IsUnique();

            modelBuilder.Entity<Brewery>().HasIndex(_ => _.Name).IsUnique();

            modelBuilder.Entity<Wholesaler>().HasIndex(_ => _.Name).IsUnique();

        }
    }
}
