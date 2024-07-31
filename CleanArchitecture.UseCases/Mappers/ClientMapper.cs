using CleanArchitecture.Entities.Clients;
using CleanArchitecture.UseCases.Dtos.ClientDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class ClientMapper
    {
        public static ClientDto MapToDto(this Client client)
        {
            return new ClientDto
            {
                ClientID = client.ClientID,
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address,
                BillingAddress = client.BillingAddress,
                IsArchived = client.IsArchived,
                PaymentTerms = client.PaymentTerms,
                CreditLimit = client.CreditLimit,
                IndustryType = client.IndustryType,
                Tax = client.Tax,
                Type = client.Type // Ajout de l'énumération
            };
        }

        public static Client MapToEntity(this CreateClientDto clientDto)
        {
            return new Client
            {
                Name = clientDto.Name,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Address = clientDto.Address,
                BillingAddress = clientDto.BillingAddress,
                IsArchived = false,
                PaymentTerms = clientDto.PaymentTerms,
                CreditLimit = clientDto.CreditLimit,
                IndustryType = clientDto.IndustryType,
                Tax = clientDto.Tax,
                Type = clientDto.Type // Ajout de l'énumération
            };
        }

        public static void MapToEntity(this UpdateClientDto updateClientDto, Client client)
        {
            client.Name = updateClientDto.Name;
            client.Email = updateClientDto.Email;
            client.Phone = updateClientDto.Phone;
            client.Address = updateClientDto.Address;
            client.BillingAddress = updateClientDto.BillingAddress;
            client.IsArchived = updateClientDto.IsArchived;
            client.PaymentTerms = updateClientDto.PaymentTerms;
            client.CreditLimit = updateClientDto.CreditLimit;
            client.IndustryType = updateClientDto.IndustryType;
            client.Tax = updateClientDto.Tax;
            client.Type = updateClientDto.Type; // Ajout de l'énumération
        }
    }
}

