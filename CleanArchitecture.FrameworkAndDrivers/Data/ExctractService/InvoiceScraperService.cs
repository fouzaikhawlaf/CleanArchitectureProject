using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.ExctractService
{
    public class InvoiceScraperService
    {
        private readonly HttpClient _httpClient;

        public InvoiceScraperService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> ScrapeInvoiceData(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(response);

            // Example: Extract specific data from the HTML
            var invoiceData = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='invoice']").InnerText;
            return invoiceData;
        }
    }
}
