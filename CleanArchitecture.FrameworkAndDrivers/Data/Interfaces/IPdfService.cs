using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FramworkAndDrivers.Data.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateSupplierPdf(IEnumerable<Supplier> suppliers);
        byte[] GenerateProductPdf(IEnumerable<Product> products);
        byte[] GenerateOrderSuppliersPdf(IEnumerable<OrderSupplier> orders);
        byte[] GenerateOrderSupplierPdf(OrderSupplier order);
        byte[] GenerateInvoicePdf(InvoiceClient invoice);

        byte[] GeneratePdf(string htmlContent);
        string SavePdfToFile(string htmlContent, string filePath);
    }
}
