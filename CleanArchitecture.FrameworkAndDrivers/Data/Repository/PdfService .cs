using CleanArchitecture.Entities.Supplier;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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
    }
}
