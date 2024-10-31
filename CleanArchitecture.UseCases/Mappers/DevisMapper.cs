using CleanArchitecture.Entities.Clients;
using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos;
using CleanArchitecture.UseCases.Dtos.OrderDtos.Devis;

using System;
using System.Linq;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class DevisMapper
    {
        // Mapping from Devis entity to DevisDto
        public static DevisDto ToDto(Devis devis)
        {
            return new DevisDto
            {
                Id = devis.Id,
                ClientId = devis.Client.ClientId,
                Products = devis.Products.Select(p => new ProductDto
                {
                    ProductID = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList(),
                TotalAmount = devis.TotalAmount,
                TVA = devis.TVA,
                TVARate = devis.TVARate,
                TotalTVA = devis.TotalTVA,
                IsAccepted = devis.IsAccepted,
                Status = devis.Status.ToString() // Convert enum to string
            };
        }

        // Mapping from CreateDevisDto to Devis entity
        public static Devis ToEntity(CreateDevisDto createDevisDto)
        {
            return new Devis
            {
                Id = createDevisDto.Id,
                Client = new Client { ClientId = createDevisDto.ClientId },
                Products = createDevisDto.Products.Select(p => new Product
                {
                    Id = p.ProductID,
                    Name = p.Name,
                    Price = p.Price
                }).ToList(),
                TotalAmount = createDevisDto.TotalAmount,
                TVA = createDevisDto.TVA,
                TotalTVA = createDevisDto.TotalTVA,
                IsAccepted = createDevisDto.IsAccepted,
                Status = Enum.Parse<DevisStatus>(createDevisDto.Status) // Convert string to enum
            };
        }

        // Mapping from UpdateDevisDto to existing Devis entity (Now returns void)
        public static void ToEntity(UpdateDevisDto updateDevisDto, Devis existingDevis)
        {
            // Update existingDevis fields directly
            existingDevis.Products = updateDevisDto.Products.Select(p => new Product
            {
                Id = p.ProductID,
                Name = p.Name,
                Price = p.Price
            }).ToList();
            existingDevis.TotalAmount = updateDevisDto.TotalAmount;
            existingDevis.TVA = updateDevisDto.TVA;
            existingDevis.TVARate = updateDevisDto.TVARate;
            existingDevis.TotalTVA = updateDevisDto.TotalTVA;
            existingDevis.IsAccepted = updateDevisDto.IsAccepted;
            existingDevis.Status = Enum.Parse<DevisStatus>(updateDevisDto.Status); // Update status

            // No return statement, since it must return void
        }
    }
}
