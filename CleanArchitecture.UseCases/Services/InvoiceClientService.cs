using Application.Mappers;
using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworkAndDrivers.Data.Services;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.InvoicesDto;
using CleanArchitecture.UseCases.Dtos.InvoicesDto.InvoiceClientDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using System;
using System.Linq;
using System.Text;

namespace CleanArchitecture.UseCases.Services
{
    public class InvoiceClientService : GenericService<InvoiceClient, InvoiceClientDto, CreateInvoiceClientDto, UpdateInvoiceClientDto>, IInvoiceClientService
    {
        private readonly IInvoiceClientRepository _invoiceClientRepository;
     
    
        private readonly WebScrapingService _webScrapingService;
        private readonly IPdfService _pdfGenerationService;

        public InvoiceClientService(
            IInvoiceClientRepository invoiceClientRepository,
            WebScrapingService webScrapingService,
            IPdfService pdfGenerationService) : base(invoiceClientRepository)
        {
            _invoiceClientRepository = invoiceClientRepository;
           
            _webScrapingService = webScrapingService;
            _pdfGenerationService = pdfGenerationService;
        }

        public async Task<InvoiceClient> GenerateInvoiceAsync(string orderClientUrl, string bonDeLivraisonUrl)
        {
            // Scraper les données depuis les URLs
            var orderClient = await _webScrapingService.ScrapeOrderClient(orderClientUrl);
            var bonDeLivraison = await _webScrapingService.ScrapeDeliveryNote(bonDeLivraisonUrl);

            // Calculer le total de la facture
            double total = bonDeLivraison.DeliveryNoteItems.Sum(item =>
            {
                var correspondingOrderItem = orderClient.OrderItems.FirstOrDefault(o => o.ProductName == item.ProductName);
                return correspondingOrderItem != null ? correspondingOrderItem.Price * item.Quantity : 0;
            });

            // Créer la facture
            var invoiceClient = new InvoiceClient
            {
                OrderClientId = orderClient.Id,
                DeliveryNoteId = bonDeLivraison.Id, // Assurez-vous que Id est correctement mappé
                TotalAmount = total,
                InvoiceDate = DateTime.Now,
                Items = orderClient.OrderItems.Select(o => new InvoiceLineItem
                {
                    ProductName = o.ProductName,
                    Quantity = bonDeLivraison.DeliveryNoteItems.FirstOrDefault(b => b.ProductName == o.ProductName)?.Quantity ?? 0,
                    Price = o.Price
                }).ToList()
            };

            // Sauvegarder la facture
            await _invoiceClientRepository.AddAsync(invoiceClient);

            // Générer le contenu HTML de la facture
            var htmlContent = BuildInvoiceHtml(invoiceClient);

            // Sauvegarder la facture PDF
            var pdfPath = $"wwwroot/pdfs/invoice_{invoiceClient.InvoiceId}.pdf"; // Changez InvoiceId par Id
            _pdfGenerationService.SavePdfToFile(htmlContent, pdfPath);

            return invoiceClient;
        }


        private string BuildInvoiceHtml(InvoiceClient invoice)
        {
            var sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append($"<h1>Facture N° {invoice.InvoiceId}</h1>"); // Changez InvoiceId par Id
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

        protected override InvoiceClientDto MapToDto(InvoiceClient entity)
        {
            return InvoiceClientMapper.MapToDto(entity);
        }

        protected override InvoiceClient MapToEntity(CreateInvoiceClientDto createDto)
        {
            return InvoiceClientMapper.MapToEntity(createDto);
        }

        protected override void MapToEntity(UpdateInvoiceClientDto updateDto, InvoiceClient entity)
        {
            InvoiceClientMapper.MapToEntity(updateDto, entity);
        }


        public string ExportInvoicesToCsv(IEnumerable<InvoiceClientDto> invoices)
        {
            try
            {
                var csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("InvoiceId,InvoiceDate,TotalAmount,Status");

                foreach (var invoice in invoices)
                {
                    // Utilisez des valeurs par défaut si nécessaire pour éviter les erreurs
                    var status = invoice.Status ?? "Unknown";

                    csvBuilder.AppendLine($"{invoice.InvoiceId},{invoice.InvoiceDate},{invoice.TotalAmount},{status}");
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "invoices.csv");
                File.WriteAllText(filePath, csvBuilder.ToString());

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return string.Empty;
            }
        }




    }
}
