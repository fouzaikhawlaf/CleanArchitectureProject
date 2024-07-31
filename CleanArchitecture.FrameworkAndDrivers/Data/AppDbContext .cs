using CleanArchitecture.Entities.Client;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Supplier;
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
        public DbSet<Supplier> Suppliers { get; set; }
       
        public DbSet<Product> Products { get; set; } // Ajouter le DbSet pour les produits

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientID);
            });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierID);
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductID);
            });
        }

    }
}