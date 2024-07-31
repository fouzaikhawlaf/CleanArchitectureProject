using CleanArchitecture.Entities.Clients;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Sales;
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
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientID);
                entity.HasMany(e => e.Sales)
                      .WithOne(s => s.Client)
                      .HasForeignKey(s => s.ClientId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductID);
                entity.HasMany(e => e.Sales)
                      .WithOne(s => s.Product)
                      .HasForeignKey(s => s.ProductId);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Client)
                      .WithMany(c => c.Sales)
                      .HasForeignKey(e => e.ClientId);
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.Sales)
                      .HasForeignKey(e => e.ProductId);
            });
        }

    }
}