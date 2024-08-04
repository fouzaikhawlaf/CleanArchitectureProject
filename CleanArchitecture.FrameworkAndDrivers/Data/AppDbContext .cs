using CleanArchitecture.Entities.Clients;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.Entities.Sales;
using CleanArchitecture.Entities.Suppliers;
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
        public DbSet<Purchase> Purchases { get; set; }

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
                      .HasForeignKey(s => s.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Client)
                      .WithMany(c => c.Sales)
                      .HasForeignKey(e => e.ClientId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.Sales)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(p => p.Product)
                      .WithMany(p => p.Purchases)
                      .HasForeignKey(p => p.ProductId);
                entity.HasOne(p => p.Supplier)
                      .WithMany(p => p.Purchases)
                      .HasForeignKey(p => p.SupplierId);
            });
        }

    }
}