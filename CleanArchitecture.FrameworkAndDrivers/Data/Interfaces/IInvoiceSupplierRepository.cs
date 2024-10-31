using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public  interface IInvoiceSupplierRepository : IGenericRepository<InvoiceSupplier>
    {
        void Add(InvoiceSupplier invoiceSupplier);  // Assure-toi que cette méthode existe
        IEnumerable<InvoiceSupplier> SearchInvoices(string searchTerm, DateTime? startDate, DateTime? endDate);
    }
}
