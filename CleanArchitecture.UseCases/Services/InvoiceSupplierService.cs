using Application.Mappers;
using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworkAndDrivers.Data.Services;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.InvoicesDto;
using CleanArchitecture.UseCases.InterfacesUse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class InvoiceSupplierService : GenericService<InvoiceSupplier, InvoiceSupplierDto, CreateInvoiceSupplierDto, UpdateInvoiceSupplierDto>, IInvoiceSupplierService
    {
        private readonly IInvoiceSupplierRepository _invoiceSupplierRepository;
      
        private readonly WebScrapingSupplierService _webScrapingService;
        private readonly IPdfService _pdfGenerationService;

        public InvoiceSupplierService(
            IInvoiceSupplierRepository invoiceSupplierRepository,
           
            WebScrapingSupplierService webScrapingService,
            IPdfService pdfGenerationService) : base(invoiceSupplierRepository)
        {
            _invoiceSupplierRepository = invoiceSupplierRepository;
         
            _webScrapingService = webScrapingService;
            _pdfGenerationService = pdfGenerationService;
        }
        public async Task<InvoiceSupplier> GenerateInvoiceAsync(string orderSupplierUrl, string bonDeReceptionUrl)
        {
            // Scraper les données depuis les URLs
            var orderSupplier = await _webScrapingService.ScrapeOrderSupplier(orderSupplierUrl); // Utilisez le suffixe Async
            var bonDeReception = await _webScrapingService.ScrapeBonDeReception(bonDeReceptionUrl); // Utilisez le suffixe Async

            // Calculer le total de la facture
            double total = bonDeReception.Items.Sum(item =>
            {
                var correspondingOrderItem = orderSupplier.Items.FirstOrDefault(o => o.ProductName == item.ProductName);
                return correspondingOrderItem != null ? correspondingOrderItem.Price * item.ReceivedQuantity : 0;
            });

            // Créer la facture
            var invoiceSupplier = new InvoiceSupplier
            {
                OrderSupplierId = orderSupplier.Id,
                BonDeReceptionId = bonDeReception.Id,
                TotalAmount = total,
                InvoiceDate = DateTime.Now,
                Items = orderSupplier.Items.Select(o => new InvoiceLineItem
                {
                    ProductName = o.ProductName,
                    Quantity = bonDeReception.Items.FirstOrDefault(b => b.ProductName == o.ProductName)?.ReceivedQuantity ?? 0,
                    Price = o.Price
                }).ToList()
            };

            // Sauvegarder la facture
            _invoiceSupplierRepository.Add(invoiceSupplier);

            // Générer le contenu HTML de la facture
            var htmlContent = BuildInvoiceHtml(invoiceSupplier);

            // Sauvegarder la facture PDF
            var pdfPath = $"wwwroot/pdfs/invoice_{invoiceSupplier.InvoiceId}.pdf";
            _pdfGenerationService.SavePdfToFile(htmlContent, pdfPath);

            return invoiceSupplier;
        }


        private string BuildInvoiceHtml(InvoiceSupplier invoice)
        {
            var sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append($"<h1>Facture N° {invoice.InvoiceNumber}</h1>");
            sb.Append($"<p>Date: {invoice.InvoiceDate.ToShortDateString()}</p>");
            sb.Append($"<p>Total: {invoice.TotalAmount} EUR</p>");
            sb.Append("<table border='1'>");
            sb.Append("<tr><th>Produit</th><th>Quantité</th><th>Prix</th><th>Total</th></tr>");

            foreach (var item in invoice.Items)
            {
                sb.Append($"<tr><td>{item.ProductName}</td><td>{item.Quantity}</td><td>{item.Price}</td><td>{item.Quantity * item.Price}</td></tr>");
            }

            sb.Append("</table>");
            sb.Append("</body></html>");

            return sb.ToString();
        }

        protected override InvoiceSupplierDto MapToDto(InvoiceSupplier entity)
        {
            return InvoiceSupplierMapper.MapToDto(entity);
        }

        protected override InvoiceSupplier MapToEntity(CreateInvoiceSupplierDto createDto)
        {
            return InvoiceSupplierMapper.MapToEntity(createDto);
        }

        protected override void MapToEntity(UpdateInvoiceSupplierDto updateDto, InvoiceSupplier entity)
        {
            InvoiceSupplierMapper.MapToEntity(updateDto, entity);
        }







        public string ExportInvoicesToCsv(IEnumerable<InvoiceSupplierDto> invoices)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("InvoiceId,OrderSupplierId,TotalAmount,InvoiceDate"); // En-tête CSV

            foreach (var invoice in invoices)
            {
                csvBuilder.AppendLine($"{invoice.InvoiceId},{invoice.TotalAmount},{invoice.InvoiceDate.ToString("o", CultureInfo.InvariantCulture)}");
            }

            // Retourner le contenu CSV sous forme de chaîne
            return csvBuilder.ToString();
        }



        public IEnumerable<InvoiceSupplierDto> SearchInvoices(string searchTerm, DateTime? startDate, DateTime? endDate)
        {
            // Appeler la fonction de recherche dans le repository pour obtenir les factures en fonction des critères
            var invoiceEntities = _invoiceSupplierRepository.SearchInvoices(searchTerm, startDate, endDate);

            // Mapper les entités vers des DTO
            var invoiceDtos = invoiceEntities.Select(entity => InvoiceSupplierMapper.MapToDto(entity)).ToList();

            // Retourner la liste des DTO
            return invoiceDtos;
        }


    }
}
