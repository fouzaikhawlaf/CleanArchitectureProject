using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class InvoiceSupplierRepository : GenericRepository<InvoiceSupplier>, IInvoiceSupplierRepository
    {
        private readonly AppDbContext _context;
        public InvoiceSupplierRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Add(InvoiceSupplier invoiceSupplier)
        {
            _context.InvoiceSuppliers.Add(invoiceSupplier);
            _context.SaveChanges();
        }


        public IEnumerable<InvoiceSupplier> SearchInvoices(string searchTerm, DateTime? startDate, DateTime? endDate)
        {
            // Récupérer toutes les factures
            IQueryable<InvoiceSupplier> invoices = _context.InvoiceSuppliers;

            // Filtrer par date si les dates sont fournies
            if (startDate.HasValue)
            {
                invoices = invoices.Where(invoice => invoice.InvoiceDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                invoices = invoices.Where(invoice => invoice.InvoiceDate <= endDate.Value);
            }

            // Filtrer par terme de recherche dans les produits
            if (!string.IsNullOrEmpty(searchTerm))
            {
                invoices = invoices.Where(invoice => invoice.Items
                    .Any(item => item.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
            }

            // Retourner les résultats
            return invoices.ToList();
        }



    }
}
