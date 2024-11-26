using CleanArchitecture.Entities.Clients;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using CleanArchitecture.Infrastructure.Data.Repository;
using CleanArchitecture.UseCases.Dtos.ClientDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class ClientService : GenericService<Client, ClientDto, CreateClientDto, UpdateClientDto>, IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository) : base(clientRepository)
        {
            _clientRepository = clientRepository;
        }

        protected override ClientDto MapToDto(Client entity)
        {
            return entity.MapToDto();
        }

        protected override Client MapToEntity(CreateClientDto createDto)
        {
            return createDto.MapToEntity();
        }

        protected override void MapToEntity(UpdateClientDto updateDto, Client entity)
        {
            updateDto.MapToEntity(entity);
        }

        public async Task<ClientDto> ArchiveClient(int clientId)
        {
            var client = await _clientRepository.ArchiveClient(clientId);
            if (client == null)
            {
                throw new KeyNotFoundException($"Client with ID {clientId} not found.");
            }
            return client.MapToDto();
        }

        public async Task<IEnumerable<ClientDto>> GetClients()
        {
            var clients = await _clientRepository.GetClients();
            return clients.Select(c => c.MapToDto());
        }
        public async Task<IEnumerable<ClientDto>> SearchClients(string query, string sortBy, bool ascending)
        {
            var clients = await _clientRepository.SearchClients(query, sortBy, ascending);
            return clients.Select(c => c.MapToDto());
        }

        public async Task<ClientDto?> UpdateClient(int id, UpdateClientDto updateDto)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                return null;
            }

            MapToEntity(updateDto, client);
            await _clientRepository.UpdateAsync(client);
            return client.MapToDto();
        }
    }
}




