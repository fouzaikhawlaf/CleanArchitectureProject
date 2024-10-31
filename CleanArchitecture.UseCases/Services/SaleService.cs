using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Sales;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.SalesDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class SaleService : GenericService<Sale, SaleDto, CreateSaleDto, UpdateSaleDto>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IClientRepository _clientRepository;

        public SaleService(ISaleRepository saleRepository, IClientRepository clientRepository) : base(saleRepository)
        {
            _saleRepository = saleRepository;
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<SaleDto>> SearchSalesAsync(string query, string sortBy, bool ascending)
        {
            var sales = await _saleRepository.GetAllAsync();
            var filteredSales = sales.Where(s => s.ProductName.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            var sortedSales = sortBy switch
            {
                "SaleDate" => ascending ? filteredSales.OrderBy(s => s.SaleDate).ToList() : filteredSales.OrderByDescending(s => s.SaleDate).ToList(),
                "TotalAmount" => ascending ? filteredSales.OrderBy(s => s.TotalAmount).ToList() : filteredSales.OrderByDescending(s => s.TotalAmount).ToList(),
                _ => filteredSales.ToList()
            };



            return sortedSales.Select(SaleMapper.ToDto);
        }

        public async Task<SaleDto> ArchiveSaleAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                throw new Exception("Sale not found.");
            }

            sale.IsArchived = true;
            await _saleRepository.UpdateAsync(sale);

            return SaleMapper.ToDto(sale);
        }

        public async Task RegisterSale(InvoiceClient invoice)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException(nameof(invoice), "Invoice cannot be null.");
            }

            var sale = SaleMapper.FromCreateDto(new CreateSaleDto
            {
                ClientId = invoice.ClientId,
                TotalAmount = invoice.TotalAmount,
                ProductName = string.Join(", ", invoice.Items.Select(i => i.ProductName)) // Joindre les noms de produits en une seule chaîne
            });

            await _saleRepository.AddAsync(sale);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesHistoryAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            return sales.Select(SaleMapper.ToDto);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByFiltersAsync(DateTime? startDate, DateTime? endDate, int? clientId, string productName)
        {
            var sales = await _saleRepository.GetAllAsync();
            if (startDate.HasValue)
                sales = sales.Where(s => s.SaleDate >= startDate.Value).ToList();
            if (endDate.HasValue)
                sales = sales.Where(s => s.SaleDate <= endDate.Value).ToList();
            if (clientId.HasValue)
                sales = sales.Where(s => s.ClientId == clientId.Value).ToList();
            if (!string.IsNullOrEmpty(productName))
                sales = sales.Where(s => s.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();

            return sales.Select(SaleMapper.ToDto);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByOrderClientIdAsync(int orderClientId)
        {
            var sales = await _saleRepository.GetAllAsync();
            var clientSales = sales.Where(s => s.ClientId == orderClientId).ToList();

            return clientSales.Select(SaleMapper.ToDto);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetAllAsync();
            var filteredSales = sales.Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate).ToList();

            return filteredSales.Select(SaleMapper.ToDto);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByClientIdAsync(int clientId)
        {
            var sales = await _saleRepository.GetAllAsync();
            var clientSales = sales.Where(s => s.ClientId == clientId).ToList();

            return clientSales.Select(SaleMapper.ToDto);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByStatusAsync(string status)
        {
            var sales = await _saleRepository.GetAllAsync();
            var filteredSales = sales.Where(s => s.Status == status).ToList();

            return filteredSales.Select(SaleMapper.ToDto);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByProductNameAsync(string productName)
        {
            var sales = await _saleRepository.GetAllAsync();
            var filteredSales = sales.Where(s => s.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredSales.Select(SaleMapper.ToDto);
        }
        public async Task<string> ExportSalesToCsvAsync(IEnumerable<SaleDto> sales)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Id,Client,Product,Amount,Date"); // En-têtes CSV

            foreach (var sale in sales)
            {
                csvBuilder.AppendLine($"{sale.Id},{sale.ClientName},{sale.ProductName},{sale.TotalAmount},{sale.SaleDate}");
            }

            var filePath = Path.Combine("path/to/your/csv/folder", $"Sales_{DateTime.Now:yyyyMMddHHmmss}.csv");
            await File.WriteAllTextAsync(filePath, csvBuilder.ToString()); // Écriture dans le fichier CSV

            return filePath; // Retourne le chemin du fichier créé
        }




        // Respecter les modificateurs d'accès 'protected'
        protected override Sale MapToEntity(CreateSaleDto createSaleDto)
        {
            // Implémentation de la méthode
            return SaleMapper.FromCreateDto(createSaleDto);
        }

        protected override void MapToEntity(UpdateSaleDto updateSaleDto, Sale sale)
        {
            // Mettre à jour l'entité 'sale' avec les données du DTO
            sale.InvoiceClientInvoiceId = updateSaleDto.InvoiceId;
            sale.ClientId = updateSaleDto.ClientId;
            sale.SaleDate = updateSaleDto.SaleDate;
            sale.TotalAmount = updateSaleDto.TotalAmount;
            sale.Status = updateSaleDto.Status;
            sale.IsArchived = updateSaleDto.IsArchived;
        }


        protected override SaleDto MapToDto(Sale sale)
        {
            // Implémentation de la méthode
            return SaleMapper.ToDto(sale);
        }
    }
}
