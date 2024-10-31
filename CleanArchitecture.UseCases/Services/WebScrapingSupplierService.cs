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
    public class WebScrapingSupplierService
    {
        private readonly HttpClient _httpClient;

        public WebScrapingSupplierService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<OrderSupplier> ScrapeOrderSupplier(string orderSupplierUrl)
        {
            var html = await _httpClient.GetStringAsync(orderSupplierUrl);
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var orderSupplier = new OrderSupplier
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

                    orderSupplier.OrderItems.Add(new OrderItem
                    {
                        ProductName = productName,
                        Quantity = quantity,
                        Price = price
                    });
                }
            }

            return orderSupplier;
        }

        public async Task<BonDeReception> ScrapeBonDeReception(string bonDeReceptionUrl)
        {
            var html = await _httpClient.GetStringAsync(bonDeReceptionUrl);
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var bonDeReception = new BonDeReception
            {
                Items = new List<BonDeReceptionItem>() // Initialiser la liste des items
            };

            // Exemple de parsing : Adapte les sélecteurs XPath ou CSS selon ta page HTML
            var receptionItems = document.DocumentNode.SelectNodes("//div[@class='reception-item']");
            if (receptionItems != null)
            {
                foreach (var item in receptionItems)
                {
                    var productName = item.SelectSingleNode(".//span[@class='product-name']")?.InnerText.Trim();
                    var quantityReceived = int.Parse(item.SelectSingleNode(".//span[@class='quantity-received']")?.InnerText.Trim());

                    bonDeReception.Items.Add(new BonDeReceptionItem
                    {
                        ProductName = productName,
                        ReceivedQuantity = quantityReceived
                    });
                }
            }

            return bonDeReception;
        }

        public async Task<InvoiceSupplierDto> ScrapeInvoiceSupplierInfoAsync(string invoiceSupplierUrl)
        {
            var html = await _httpClient.GetStringAsync(invoiceSupplierUrl);
            var document = new HtmlDocument();
            document.LoadHtml(html);

            // Scrape supplier information
            var supplierNameNode = document.DocumentNode.SelectSingleNode("//span[@class='supplier-name']");
            var supplierAddressNode = document.DocumentNode.SelectSingleNode("//span[@class='supplier-address']");
            if (supplierNameNode == null || supplierAddressNode == null)
                throw new Exception("Supplier information not found.");

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
            return new InvoiceSupplierDto
            {
                SupplierName = supplierNameNode.InnerText.Trim(),
                SupplierAddress = supplierAddressNode.InnerText.Trim(),
                TaxRate = (double)taxRate,
                DeliveryDetails = deliveryInfo,
                LineItems = products
            };
        }
    }
}
