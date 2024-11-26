using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: api/service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServices()
        {
            var services = await _serviceService.GetAllAsync();
            return Ok(services);
        }

        // GET: api/service/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceDto>> GetServiceById(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        // POST: api/service
        [HttpPost]
        public async Task<ActionResult<ServiceDto>> CreateService(CreateServiceDto createServiceDto)
        {
            var service = await _serviceService.AddAsync(createServiceDto);
            return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, service);
        }

        // PUT: api/service/{id:int}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateService([FromRoute] int id, [FromBody] UpdateServiceDto serviceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Appel de la méthode UpdateAsync sans assignation
                await _serviceService.UpdateAsync(id, serviceDto);

                // Si l'entité est mise à jour avec succès, retourner NoContent
                return NoContent(); // 204 No Content pour indiquer que la mise à jour a réussi
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Retourner un message de non trouvé
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        // DELETE: api/service/{id:int}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                // Appel de la méthode DeleteAsync
                await _serviceService.DeleteAsync(id);

                // Retourner NoContent pour indiquer que la suppression a réussi
                return NoContent(); // 204 No Content
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Retourner un message de non trouvé
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }



        // GET: api/service/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> SearchServices([FromQuery] string searchTerm)
        {
            var services = await _serviceService.SearchServicesAsync(searchTerm);
            return Ok(services);
        }

        // POST: api/service/archive/{id:int}
        [HttpPost("archive/{id:int}")]
        public async Task<ActionResult<ServiceDto>> ArchiveService(int id)
        {
            var archivedService = await _serviceService.ArchiveServiceAsync(id);
            if (archivedService == null)
            {
                return NotFound();
            }
            return Ok(archivedService);
        }
    }
}
