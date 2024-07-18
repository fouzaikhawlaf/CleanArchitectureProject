using CleanArchitecture.Entities.Client;
using Microsoft.EntityFrameworkCore;
using System;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace CleanArchitecture.FrameworksAndDrivers
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        // Ajoutez d'autres DbSet pour vos autres entités ici


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientID);
            });

        }

    }
}