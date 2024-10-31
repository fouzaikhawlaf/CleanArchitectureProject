using CleanArchitecture.Entities.Orders;
using CleanArchitecture.Entities.Orders.DeliveryNotes;
using CleanArchitecture.UseCases.Dtos.InvoicesDto;
using CleanArchitecture.UseCases.InterfacesUse;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Services
{
    public class WebScrapingService
    {
        private readonly HttpClient _httpClient;
        private const string ExternalDataUrl = "https://example.com/client-info"; // Remplacer par l'URL réelle

        public WebScrapingService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<OrderClient> ScrapeOrderClient(string orderClientUrl)
        {
            var html = await _httpClient.GetStringAsync(orderClientUrl);
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var orderClient = new OrderClient
            {
                OrderItems = new List<OrderItem>() // Initialiser la liste des items
            };

            // Exemple de parsing : Adapte les sélecteurs XPath ou CSS selon ta page HTML
            var orderItems = document.DocumentNode.SelectNodes("//div[@class='order-item']");
            if (orderItems != null)
            {
                foreach (var item in orderItems)
                {
                    var productName = item.SelectSingleNode(".//span[@class='product-name']")?.InnerText.Trim();
                    var quantity = int.Parse(item.SelectSingleNode(".//span[@class='quantity']")?.InnerText.Trim());
                    var price = double.Parse(item.SelectSingleNode(".//span[@class='price']")?.InnerText.Trim().Replace("€", "").Trim());

                    orderClient.OrderItems.Add(new OrderItem
                    {
                        ProductName = productName,
                        Quantity = quantity,
                        Price = price
                    });
                }
            }

            return orderClient;
        }

        public async Task<DeliveryNote> ScrapeDeliveryNote(string deliveryNoteUrl)
        {
            var html = await _httpClient.GetStringAsync(deliveryNoteUrl);
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var deliveryNote = new DeliveryNote
            {
                DeliveryNoteItems = new List<DeliveryItem>() // Initialiser la liste des items
            };

            // Exemple de parsing : Adapte les sélecteurs XPath ou CSS selon ta page HTML
            var deliveryItems = document.DocumentNode.SelectNodes("//div[@class='delivery-item']");
            if (deliveryItems != null)
            {
                foreach (var item in deliveryItems)
                {
                    var productName = item.SelectSingleNode(".//span[@class='product-name']")?.InnerText.Trim();
                    var quantityReceived = int.Parse(item.SelectSingleNode(".//span[@class='quantity-received']")?.InnerText.Trim());

                    deliveryNote.DeliveryNoteItems.Add(new DeliveryItem
                    {
                        ProductName = productName,
                        Quantity = quantityReceived
                    });
                }
            }

            return deliveryNote;
        }

        public async Task<InvoiceClientDto> ScrapeClientInfoAsync()
        {
            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(ExternalDataUrl);

            // Scrape client information
            var clientNameNode = document.DocumentNode.SelectSingleNode("//span[@class='client-name']");
            var clientAddressNode = document.DocumentNode.SelectSingleNode("//span[@class='client-address']");
            if (clientNameNode == null || clientAddressNode == null)
                throw new Exception("Client information not found.");

            // Scrape tax rate
            var taxRateNode = document.DocumentNode.SelectSingleNode("//span[@class='tax-rate-value']");
            if (taxRateNode == null)
                throw new Exception("Tax rate not found.");
            var taxRateText = taxRateNode.InnerText.Trim('%');
            if (!decimal.TryParse(taxRateText, out decimal taxRate))
                throw new Exception("Failed to parse tax rate.");

            // Scrape delivery details
            var deliveryNode = document.DocumentNode.SelectSingleNode("//div[@class='delivery-details']");
            if (deliveryNode == null)
                throw new Exception("Delivery details not found.");
            var deliveryInfo = deliveryNode.InnerText.Trim();

            // Scrape product information
            var productNodes = document.DocumentNode.SelectNodes("//div[@class='product-item']");
            var products = new List<InvoiceItemDto>();
            if (productNodes != null)
            {
                foreach (var productNode in productNodes)
                {
                    var productNameNode = productNode.SelectSingleNode(".//span[@class='product-name']");
                    var productPriceNode = productNode.SelectSingleNode(".//span[@class='product-price']");
                    var productQuantityNode = productNode.SelectSingleNode(".//span[@class='product-quantity']");

                    if (productNameNode == null || productPriceNode == null || productQuantityNode == null) continue;

                    if (double.TryParse(productPriceNode.InnerText, out double productPrice) &&
                        int.TryParse(productQuantityNode.InnerText, out int productQuantity))
                    {
                        products.Add(new InvoiceItemDto
                        {
                            ProductName = productNameNode.InnerText.Trim(),
                            Price = productPrice,
                            Quantity = productQuantity
                        });
                    }
                }
            }

            // Return scraped data
            return new InvoiceClientDto
            {
                ClientName = clientNameNode.InnerText.Trim(),
                ClientAddress = clientAddressNode.InnerText.Trim(),
                TaxRate = (double)taxRate,
                DeliveryDetails = deliveryInfo,
                Products = products
            };
        }
    }
}
