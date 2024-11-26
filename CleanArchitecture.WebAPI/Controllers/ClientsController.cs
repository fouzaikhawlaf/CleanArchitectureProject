using CleanArchitecture.Entities.Clients;
using CleanArchitecture.FrameworkAndDrivers.Exceptions;
using CleanArchitecture.UseCases.Dtos.ClientDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
   
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientsController> _logger;

        // Inject ILogger<ClientsController> into the constructor
        public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
        {
            _clientService = clientService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));  // Ensure logger is not null
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
        {
            try
            {
                // Fetch all clients without sorting or pagination
                var clients = await _clientService.GetClients();
                return Ok(clients);
            }
            catch (Exception)
            {
                // Handle exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            try
            {
                var result = await _clientService.GetByIdAsync(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception )
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> CreateClient([FromBody] CreateClientDto clientDto)
        {
            try
            {
                if (clientDto == null)
                {
                    _logger.LogWarning("ClientDto is null");
                    return BadRequest("Client data is required.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ModelState is invalid: {Errors}",
                        string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                    return BadRequest(ModelState);
                }

                var createdClient = await _clientService.AddAsync(clientDto);

                if (createdClient == null)
                {
                    _logger.LogError("Error creating client.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error creating client.");
                }

                _logger.LogInformation("Client created successfully with ID: {ClientID}", createdClient.ClientID);

                return CreatedAtAction(nameof(GetClient), new { id = createdClient.ClientID }, createdClient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the client.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new client record.");
            }
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult<ClientDto>> UpdateClient([FromRoute] int id, [FromBody] UpdateClientDto clientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var clientToUpdate = await _clientService.UpdateClient(id, clientDto);

                if (clientToUpdate == null)
                {
                    return NotFound($"Client with Id = {id} not found");
                }

                // Validate the workflow after updating the client
              

                return Ok(clientToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ClientDto>> DeleteClient(int id)
        {
            try
            {
                var clientToDelete = await _clientService.GetByIdAsync(id);

                if (clientToDelete == null)
                {
                    return NotFound($"Client with Id = {id} not found");
                }

                await _clientService.DeleteAsync(id);

                return Ok(clientToDelete); // Optionally, you can return the deleted client.
            }
            catch (Exception )
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ClientDto>>> SearchClients([FromQuery] string query, [FromQuery] string sortBy = "Name", [FromQuery] bool ascending = true)
        {
            try
            {
                var clients = await _clientService.SearchClients(query, sortBy, ascending);
                return Ok(clients);
            }
            catch (Exception )
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error searching clients");
            }
        }

        [HttpPut("archive/{id:int}")]
        public async Task<ActionResult<ClientDto>> ArchiveClient(int id)
        {
            try
            {
                var archivedClient = await _clientService.ArchiveClient(id);

                if (archivedClient == null)
                {
                    return NotFound($"Client with Id = {id} not found");
                }

             
              

                return Ok(archivedClient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error archiving client: {ex.Message}");
            }
        }

        private void ValidateClient(Client client)
        {
            if (string.IsNullOrEmpty(client.Name))
                throw new InvalidEntityException("Client name cannot be null or empty.");
            if (string.IsNullOrEmpty(client.Email))
                throw new InvalidEntityException("Client email cannot be null or empty.");
            if (string.IsNullOrEmpty(client.Phone))
                throw new InvalidEntityException("Client phone cannot be null or empty.");
            if (string.IsNullOrEmpty(client.Address))
                throw new InvalidEntityException("Client address cannot be null or empty.");
            if (string.IsNullOrEmpty(client.BillingAddress))
                throw new InvalidEntityException("Client billing address cannot be null or empty.");
            if (string.IsNullOrEmpty(client.IndustryType))
                throw new InvalidEntityException("Client industry type cannot be null or empty.");
            if (string.IsNullOrEmpty(client.Tax))
                throw new InvalidEntityException("Client tax cannot be null or empty.");
        }

        

       



    }
}
    
    

