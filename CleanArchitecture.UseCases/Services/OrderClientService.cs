using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworkAndDrivers.Data.Repository;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.OrderDtos;

using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class OrderClientService : GenericService<OrderClient, OrderClientDto, CreateOrderClientDto, UpdateOrderClientDto>, IOrderClientService
    {
        private readonly IOrderClientRepository _orderClientRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProduitSRepository _serviceRepository; // Assurez-vous d'importer le bon namespace
        private readonly IProductRepository _productRepository; // Assurez-vous d'importer le bon namespace

        public OrderClientService(IOrderClientRepository orderClientRepository, IClientRepository clientRepository,IProductRepository productRepository, IProduitSRepository serviceRepository)
            : base(orderClientRepository)
        {
            _orderClientRepository = orderClientRepository;
            _productRepository = productRepository;
            _serviceRepository = serviceRepository;
            _clientRepository = clientRepository;
        }

        protected override OrderClientDto MapToDto(OrderClient entity)
        {
            return entity.MapToDto();
        }

        protected override OrderClient MapToEntity(CreateOrderClientDto createDto)
        {
            return createDto.MapToEntity();
        }

        protected override void MapToEntity(UpdateOrderClientDto updateDto, OrderClient entity)
        {
            updateDto.MapToEntity(entity);
        }

        public async Task ArchiveAsync(int id)
        {
            await _orderClientRepository.ArchiveAsync(id);
        }

        public double CalculateTotalAmount(OrderClientDto orderClientDto)
        {
            // Sum the price * quantity for all items
            double totalAmount = orderClientDto.OrderItems.Sum(item => item.Price * item.Quantity);
            // Calculate total TVA
            double totalTVA = orderClientDto.OrderItems.Sum(item => item.TVA);
            // Subtract discount and return final total
            return totalAmount + totalTVA - orderClientDto.Discount;
        }

        public async Task<List<OrderClientDto>> SearchAsync(string keyword)
        {
            var orders = await _orderClientRepository.SearchAsync(keyword);
            return orders.Select(order => order.MapToDto()).ToList();
        }

        public async Task ConfirmOrderAsync(int id)
        {
            await _orderClientRepository.ConfirmOrderClientAsync(id);
        }


        public async Task<IEnumerable<OrderClientDto>> GetOrdersByClientIdAsync(int clientId)
        {
            var orders = await _orderClientRepository.GetOrdersByClientIdAsync(clientId);
            return orders.Select(order => order.MapToDto()).ToList();
        }

        public async Task<IEnumerable<OrderClientDto>> SearchOrdersAsync(string searchTerm)
        {
            var searchResults = await _orderClientRepository.SearchOrdersAsync(searchTerm);
            return searchResults.Select(order => order.MapToDto()).ToList(); // Mapping to DTO
        }


        public async Task<IEnumerable<OrderClientDto>> GetPendingOrdersAsync()
        {
            var pendingOrders = await _orderClientRepository.GetPendingOrdersAsync();
            return pendingOrders.Select(order => order.MapToDto()).ToList(); // Mapping to DTO
        }

        public async Task<IEnumerable<OrderClientDto>> GetArchivedOrdersAsync()
        {
            var archivedOrders = await _orderClientRepository.GetArchivedOrdersAsync();
            return archivedOrders.Select(order => order.MapToDto()).ToList(); // Mapping to DTO
        }

        public async Task ArchiveOrderAsync(int id)
        {
            // Call the repository to archive the order
            await _orderClientRepository.ArchiveAsync(id);
        }


        public async Task<OrderClientDto> CreateOrderAsync(CreateOrderClientDto createOrderDto)
        {
            // Étape 2 : Vérification des informations du client
            var client = await _clientRepository.GetByIdAsync(createOrderDto.ClientId);
            if (client == null)
            {
                throw new ArgumentException("Client not found.");
            }

            // Étape 3 : Ajout des produits ou services à la commande
            var order = new OrderClient
            {
                ClientID = createOrderDto.ClientId,
                ClientName = client.Name, // Supposons que Client a une propriété Name
                OrderDate = createOrderDto.OrderDate,
                Discount = createOrderDto.Discount
            };

            foreach (var itemDto in createOrderDto.OrderItems)
            {
                // Supposons que vous avez une méthode pour récupérer les détails du produit ou du service par ID
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
                var service = await _serviceRepository.GetByIdAsync(itemDto.ProductId); // Assurez-vous d'avoir un dépôt pour les services

                if (product == null && service == null)
                {
                    throw new ArgumentException($"Product or service with ID {itemDto.ProductId} not found.");
                }

                var orderItem = new OrderItem
                {
                    ProductId = product?.Id ?? service?.Id ?? 0, // Utilise l'ID du produit ou du service
                    ProductName = product?.Name ?? service?.Name ?? "Unknown", // Utilise le nom du produit ou du service
                    Quantity = itemDto.Quantity,
                    Price = product?.Price ?? service?.Price ?? 0, // Récupérer le prix du produit ou du service
                    TVARate = product?.TVARate ?? service?.TVARate ?? TVAType.None, // Récupérer le taux de TVA
                    TVA = CalculateTVA((product?.Price ?? service?.Price ?? 0),
                                       (product != null ? product.TVARate : service?.TVARate ?? TVAType.None),
                                       itemDto.Quantity) // Calculez la TVA ici
                };

                order.OrderItems.Add(orderItem);
            }

            // Étape 4 : Calcul du total de la commande
            CalculateOrderTotals(order);

            // Étape 5 : Sauvegarde de la commande dans la base de données
            await _orderClientRepository.AddAsync(order);
            await _orderClientRepository.SaveChangesAsync();

            var orderDto = order.MapToDto(); // Assurez-vous d'utiliser la méthode de mappage ici
            return orderDto;

        }



        public async Task<OrderClientDto> ValidateOrder(int orderId)
        {
            // Étape 1 : Récupérer la commande
            var order = await _orderClientRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException("Order not found.");
            }

            // Étape 2 : Vérifier l'état de la commande
            if (order.Status == OrderState.Confirmed)
            {
                throw new InvalidOperationException("Order is already validated.");
            }

            // Étape 3 : Mettre à jour l'état de la commande
            order.Status = OrderState.Confirmed; // Mettez à jour l'état
            order.IsArchived = false; // Définir l'archivage si nécessaire

            // Étape 4 : Enregistrer les changements
            await _orderClientRepository.SaveChangesAsync();

            // Étape 5 : Mapper la commande validée à DTO
            return order.MapToDto(); // Utilisez la méthode de mappage
        }



        private double CalculateTVA(double price, TVAType tvaRate, int quantity)
        {
            // Implémentez la logique pour calculer la TVA selon le taux
            double tvaPercentage = tvaRate switch
            {
                TVAType.TVA5 => 0.05,
                TVAType.SevenPercent => 0.07,
                TVAType.NineteenPercent => 0.19,
                _ => 0
            };

            return price * tvaPercentage * quantity;
        }

        private void CalculateOrderTotals(OrderClient order)
        {
            // Calculer le total et la TVA
            order.TotalAmount = order.OrderItems.Sum(i => (i.Price * i.Quantity) + i.TVA);
            order.TotalTVA = order.OrderItems.Sum(i => i.TVA);
            order.TotalAmount -= order.Discount; // Appliquer la remise
        }


        public async Task<byte[]> GenerateOrderPdfAsync(int orderId)
        {
            // Étape 1 : Récupérer la commande à partir de l'ID
            var order = await _orderClientRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException("Order not found.");
            }

            // Étape 2 : Créer un document PDF
            using (var memoryStream = new MemoryStream())
            {
                Document document = new Document();
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Étape 3 : Ajouter du contenu au PDF
                document.Add(new iTextSharp.text.Paragraph($"Order ID: {order.Id}"));
                document.Add(new iTextSharp.text.Paragraph($"Client Name: {order.ClientName}"));
                document.Add(new iTextSharp.text.Paragraph($"Order Date: {order.OrderDate.ToShortDateString()}"));
                document.Add(new iTextSharp.text.Paragraph($"Discount: {order.Discount}"));
                document.Add(new iTextSharp.text.Paragraph($"Total Amount: {order.TotalAmount}"));
                document.Add(new iTextSharp.text.Paragraph($"Total TVA: {order.TotalTVA}"));
                document.Add(new iTextSharp.text.Paragraph("Items:"));

                // Étape 4 : Ajouter les items de la commande
                PdfPTable table = new PdfPTable(4); // Nombre de colonnes
                table.AddCell("Product Name");
                table.AddCell("Quantity");
                table.AddCell("Price");
                table.AddCell("TVA");

                foreach (var item in order.OrderItems)
                {
                    table.AddCell(item.ProductName);
                    table.AddCell(item.Quantity.ToString());
                    table.AddCell(item.Price.ToString("C"));
                    table.AddCell(item.TVA.ToString("P"));
                }

                document.Add(table);
                document.Close();

                // Étape 5 : Retourner le contenu du PDF
                return memoryStream.ToArray();
            }
        }




    }


}

