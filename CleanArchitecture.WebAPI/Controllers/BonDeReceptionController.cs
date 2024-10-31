using CleanArchitecture.FrameworkAndDrivers.Exceptions;
using CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto;
using CleanArchitecture.UseCases.InterfacesUse;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonDeReceptionController : ControllerBase
    {
        private readonly IBonDeReceptionService _bonDeReceptionService;

        public BonDeReceptionController(IBonDeReceptionService bonDeReceptionService)
        {
            _bonDeReceptionService = bonDeReceptionService;
        }

        // Récupérer tous les bons de réception
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BonDeReceptionDto>>> GetAll()
        {
            try
            {
                var bonDeReceptions = await _bonDeReceptionService.GetAllAsync();
                return Ok(bonDeReceptions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Récupérer un bon de réception par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<BonDeReceptionDto>> GetById(int id)
        {
            try
            {
                var bonDeReception = await _bonDeReceptionService.GetByIdAsync(id);
                if (bonDeReception == null)
                {
                    throw new BonDeReceptionNotFoundException(id.ToString());
                }
                return Ok(bonDeReception);
            }
            catch (BonDeReceptionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Créer un bon de réception à partir d'une commande fournisseur
        [HttpPost("from-order/{orderSupplierId}")]
        public async Task<ActionResult> CreateFromOrder(int orderSupplierId)
        {
            try
            {
                await _bonDeReceptionService.CreateBonDeReceptionFromOrderSupplierAsync(orderSupplierId);
                return Ok();
            }
            catch (OrderSupplierNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

       

        // Mettre à jour un bon de réception
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateBonDeReceptionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _bonDeReceptionService.UpdateAsync(id, dto);
                return Ok();
            }
            catch (BonDeReceptionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Confirmer un bon de réception
        [HttpPost("{id}/confirm")]
        public async Task<ActionResult> Confirm(int id)
        {
            try
            {
                await _bonDeReceptionService.ConfirmBonDeReceptionAsync(id);
                return Ok();
            }
            catch (BonDeReceptionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Archiver un bon de réception
        [HttpPost("{id}/archive")]
        public async Task<ActionResult> Archive(int id)
        {
            try
            {
                await _bonDeReceptionService.ArchiveBonDeReceptionAsync(id);
                return Ok();
            }
            catch (BonDeReceptionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Gérer les écarts de réception
        [HttpPost("{id}/handle-discrepancy")]
        public async Task<ActionResult> HandleDiscrepancy(int id, [FromBody] double discrepancyAmount)
        {
            try
            {
                await _bonDeReceptionService.HandleReceptionDiscrepanciesAsync(id, discrepancyAmount);
                return Ok();
            }
            catch (BonDeReceptionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Récupérer tous les bons de réception archivés
        [HttpGet("archived")]
        public async Task<ActionResult<IEnumerable<BonDeReceptionDto>>> GetArchived()
        {
            try
            {
                var archivedBonDeReceptions = await _bonDeReceptionService.GetArchivedBonDeReceptionsAsync();
                return Ok(archivedBonDeReceptions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }
    }
}
