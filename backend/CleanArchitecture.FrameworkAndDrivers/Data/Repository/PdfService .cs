using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Supplier;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FramworkAndDrivers.Data.Repository
{
    public class PdfService : IPdfService
    {
        private readonly ILogger<PdfService> _logger;

        public PdfService(ILogger<PdfService> logger)
        {
            _logger = logger;
        }
        public byte[] GenerateSupplierPdf(IEnumerable<Supplier> suppliers)
        {
            using (var stream = new MemoryStream())
            {
                // Initialize PDF writer and document
                PdfWriter writer = new PdfWriter(stream);
                iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);

                // Add content to the document
                foreach (var supplier in suppliers)
                {
                    document.Add(new Paragraph($"Supplier ID: {supplier.SupplierID}"));
                    document.Add(new Paragraph($"Name: {supplier.Name}"));
                    document.Add(new Paragraph($"Email: {supplier.Email}"));
                    document.Add(new Paragraph($"Phone: {supplier.Phone}"));
                    document.Add(new Paragraph($"Address: {supplier.Address}"));
                    document.Add(new Paragraph($"Total Chiffre D'Affaire: {supplier.TotalChiffreDAffaire}"));
                    document.Add(new Paragraph($"Payment Terms: {supplier.PaymentTerms}"));
                    document.Add(new Paragraph($"Supplier Type: {supplier.SupplierType}"));
                    document.Add(new Paragraph("------------------------------------------------------"));
                }

                // Close the document
                document.Close();
                return stream.ToArray();
            }
        }

        public byte[] GenerateProductPdf(IEnumerable<Product> products)
        {
            try
            {
                _logger.LogInformation("Starting PDF generation for products");

                using (var stream = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    if (!products.Any())
                    {
                        _logger.LogWarning("No products to generate PDF for.");
                        throw new Exception("No products to generate PDF for.");
                    }

                    foreach (var product in products)
                    {
                        document.Add(new Paragraph($"Product ID: {product.ProductID}"));
                        document.Add(new Paragraph($"Name: {product.Name}"));
                        document.Add(new Paragraph($"Description: {product.Description}"));
                        document.Add(new Paragraph($"Price: {product.Price}"));
                        document.Add(new Paragraph($"Product Type: {product.ProductType}"));
                        document.Add(new Paragraph("------------------------------------------------------"));
                    }

                    document.Close();
                    _logger.LogInformation("Finished PDF generation for products");
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating product PDF");
                throw;
            }
        }

    }
}
    
    

