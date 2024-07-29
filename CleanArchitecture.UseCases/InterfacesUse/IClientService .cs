using CleanArchitecture.Entities.Client;
using CleanArchitecture.UseCases.Dtos.ClientDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IClientService : IGenericService<Client, ClientDto, CreateClientDto, UpdateClientDto>
    {
        Task<ClientDto> ArchiveClient(int clientId);
        Task<IEnumerable<ClientDto>> GetClients(string sortBy, bool ascending);
        Task<IEnumerable<ClientDto>> SearchClients(string query, string sortBy, bool ascending);
        Task<ClientDto?> UpdateClient(int id, UpdateClientDto updateDto);

    }
    
}