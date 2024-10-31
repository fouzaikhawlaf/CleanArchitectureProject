using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Sales;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Stock> GetStockByProductIdAsync(int productId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);
        }


        // Vérifier la disponibilité du stock dans la base de données
        public bool VerifierDisponibilite(int produitId, int quantite)
        {
            var produit = _context.Products.SingleOrDefault(p => p.Id == produitId);
            if (produit != null)
            {
                return produit.Quantity >= quantite;
            }
            return false;
        }

        // Dédure le stock dans la base de données
        public void DeductionStock(int produitId, int quantite)
        {
            var produit = _context.Products.SingleOrDefault(p => p.Id == produitId);
            if (produit != null)
            {
                produit.Quantity -= quantite;
                _context.SaveChanges();
            }
        }
    }
}

