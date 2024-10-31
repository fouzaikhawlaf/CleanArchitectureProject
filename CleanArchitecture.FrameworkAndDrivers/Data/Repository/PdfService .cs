using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Suppliers;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Invoices;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace CleanArchitecture.FramworkAndDrivers.Data.Repository
{
    public class PdfService : IPdfService
    {
        private readonly ILogger<PdfService> _logger;
        private readonly IConverter _pdfConverter;
        public PdfService(ILogger<PdfService> logger, IConverter pdfConverter)
        {
            _logger = logger;
            _pdfConverter = pdfConverter;
        }

        public byte[] GenerateSupplierPdf(IEnumerable<Supplier> suppliers)
        {
            try
            {
                _logger.LogInformation("Starting PDF generation for suppliers");

                using (var stream = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    foreach (var supplier in suppliers)
                    {
                        document.Add(new Paragraph($"Supplier ID: {supplier.SupplierId}"));
                        document.Add(new Paragraph($"Name: {supplier.Name}"));
                        document.Add(new Paragraph($"Email: {supplier.Email}"));
                        document.Add(new Paragraph($"Phone: {supplier.Phone}"));
                        document.Add(new Paragraph($"Address: {supplier.Address}"));
                        document.Add(new Paragraph($"Total Chiffre D'Affaire: {supplier.TotalChiffreDAffaire}"));
                        document.Add(new Paragraph($"Payment Terms: {supplier.PaymentTerms}"));
                        document.Add(new Paragraph($"Supplier Type: {supplier.SupplierType}"));
                        document.Add(new Paragraph("------------------------------------------------------"));
                    }

                    document.Close();
                    _logger.LogInformation("Finished PDF generation for suppliers");
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating supplier PDF");
                throw;
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
                        document.Add(new Paragraph($"Product ID: {product.Id}"));
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

        public byte[] GenerateOrderSupplierPdf(OrderSupplier order)
        {
            try
            {
                if (order == null)
                {
                    _logger.LogWarning("Order purchase is null.");
                    throw new Exception("Order purchase cannot be null.");
                }

                _logger.LogInformation($"Starting PDF generation for Order Purchase ID: {order.Id}");

                using (var stream = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    document.Add(new Paragraph($"Order Purchase ID: {order.Id}"));
                    document.Add(new Paragraph($"Order Date: {order.OrderDate.ToShortDateString()}"));
                    document.Add(new Paragraph($"Total Amount: {order.TotalAmount:C}"));
                    document.Add(new Paragraph($"Total TVA: {order.TotalTVA:C}"));
                    document.Add(new Paragraph($"Promotion: {order.Promotion}%"));
                    document.Add(new Paragraph($"Is Archived: {order.IsArchived}"));
                    document.Add(new Paragraph("------------------------------------------------------"));

                    foreach (var item in order.OrderItems)
                    {
                        document.Add(new Paragraph($"Product ID: {item.ProductId}"));
                        document.Add(new Paragraph($"Product Name: {item.Product.Name}"));
                        document.Add(new Paragraph($"Unit Price: {item.Price:C}"));
                        document.Add(new Paragraph($"Quantity: {item.Quantity}"));
                        document.Add(new Paragraph($"TVA: {item.TVA:C}"));
                        document.Add(new Paragraph("------------------------------------------------------"));
                    }

                    document.Close();
                    _logger.LogInformation($"Finished PDF generation for Order Purchase ID: {order.Id}");
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating order purchase PDF");
                throw;
            }
        }


        public byte[] GenerateOrderSuppliersPdf(IEnumerable<OrderSupplier> orders)
        {
            try
            {
                _logger.LogInformation("Starting PDF generation for order purchases");

                using (var stream = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    if (!orders.Any())
                    {
                        _logger.LogWarning("No order purchases to generate PDF for.");
                        throw new Exception("No order purchases to generate PDF for.");
                    }

                    foreach (var order in orders)
                    {
                        document.Add(new Paragraph($"Order Purchase ID: {order.Id}"));
                        document.Add(new Paragraph($"Order Date: {order.OrderDate.ToShortDateString()}"));
                        document.Add(new Paragraph($"Total Amount: {order.TotalAmount:C}"));
                        document.Add(new Paragraph($"Total TVA: {order.TotalTVA:C}"));
                        document.Add(new Paragraph($"Promotion: {order.Promotion}%"));
                        document.Add(new Paragraph($"Is Archived: {order.IsArchived}"));
                        document.Add(new Paragraph("------------------------------------------------------"));

                        foreach (var item in order.OrderItems)
                        {
                            document.Add(new Paragraph($"Product ID: {item.ProductId}"));
                            document.Add(new Paragraph($"Product Name: {item.Product.Name}"));
                            document.Add(new Paragraph($"Unit Price: {item.Price:C}"));
                            document.Add(new Paragraph($"Quantity: {item.Quantity}"));
                            document.Add(new Paragraph($"TVA: {item.TVA:C}"));
                            document.Add(new Paragraph("------------------------------------------------------"));
                        }
                    }

                    document.Close();
                    _logger.LogInformation("Finished PDF generation for order purchases");
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating order purchases PDF");
                throw;
            }
        }


        public byte[] GenerateInvoicePdf(InvoiceClient invoice)
        {
            using (var stream = new MemoryStream())
            {
                var pdfWriter = new PdfWriter(stream);
                var pdf = new PdfDocument(pdfWriter);
                var document = new Document(pdf);

                document.Add(new Paragraph("Invoice").SetFontSize(24));

                // Add client info
                document.Add(new Paragraph($"Client: {invoice.ClientName}"));
                document.Add(new Paragraph($"Address: {invoice.ClientAddress}"));
                document.Add(new Paragraph($"Delivery Details: {invoice.DeliveryDetails}"));

                // Add table for invoice items
                var table = new Table(4);
                table.AddHeaderCell("Product Name");
                table.AddHeaderCell("Quantity");
                table.AddHeaderCell("Unit Price");
                table.AddHeaderCell("Total");

                foreach (var item in invoice.Items)
                {
                    table.AddCell(item.ProductName);
                    table.AddCell(item.Quantity.ToString());
                    table.AddCell(item.Price.ToString("C"));
                    table.AddCell(item.Total.ToString("C"));
                }

                document.Add(table);

                // Add total amount with tax
                document.Add(new Paragraph($"Total Amount: {invoice.TotalAmount:C}"));
                document.Add(new Paragraph($"Tax Rate: {invoice.TaxRate}%"));
                document.Add(new Paragraph($"Total Amount (with tax): {invoice.TotalAmountWithTax:C}"));

                document.Close();

                return stream.ToArray(); // Return the generated PDF as a byte array
            }
        }
        public byte[] GeneratePdf(string htmlContent)
        {
            var pdfDoc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = DinkToPdf.ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = DinkToPdf.PaperKind.A4,
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return _pdfConverter.Convert(pdfDoc);
        }

        public string SavePdfToFile(string htmlContent, string filePath)
        {
            byte[] pdfBytes = GeneratePdf(htmlContent);
            File.WriteAllBytes(filePath, pdfBytes);
            return filePath;
        }
    }

}

