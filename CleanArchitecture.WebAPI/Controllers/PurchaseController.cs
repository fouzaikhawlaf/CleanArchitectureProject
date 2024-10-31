using CleanArchitecture.Entities.Invoices; // Assurez-vous d'ajouter cet espace de noms
using CleanArchitecture.FrameworkAndDrivers.Exceptions;
using CleanArchitecture.UseCases.Dtos.PurchaseDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        // GET: api/purchase/history
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchasesHistory()
        {
            var purchases = await _purchaseService.GetPurchasesHistoryAsync();
            return Ok(purchases); // Retourne les achats en statut 200 OK
        }

        // GET: api/purchase
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetAllPurchases()
        {
            var purchases = await _purchaseService.GetPurchasesHistoryAsync();
            return Ok(purchases); // Retourne tous les achats
        }

        // GET: api/purchase/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchaseById(int id)
        {
            try
            {
                var purchase = await _purchaseService.GetByIdAsync(id);
                return Ok(purchase); // Retourne l'achat
            }
            catch (PurchaseNotFoundException ex)
            {
                return NotFound(ex.Message); // Retourne un statut 404 Not Found avec message personnalisé
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Retourne un statut 500 en cas d'erreur
            }
        }

        // POST: api/purchase
        [HttpPost]
        public async Task<ActionResult<PurchaseDto>> RegisterPurchase([FromBody] InvoiceSupplier invoice)
        {
            if (invoice == null)
            {
                return BadRequest("Invoice cannot be null."); // Retourne un statut 400 Bad Request
            }

            await _purchaseService.RegisterPurchaseAsync(invoice);
            return CreatedAtAction(nameof(GetPurchasesHistory), new { /* Vous pouvez inclure l'ID ou d'autres paramètres ici */ }); // Retourne un statut 201 Created
        }

        // PUT: api/purchase/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<PurchaseDto>> UpdatePurchase(int id, [FromBody] UpdatePurchaseDto updatePurchaseDto)
        {
            if (updatePurchaseDto == null)
            {
                return BadRequest("Purchase data cannot be null."); // Retourne un statut 400 Bad Request
            }

            try
            {
                var purchase = await _purchaseService.GetByIdAsync(id);
                if (purchase == null)
                {
                    throw new PurchaseNotFoundException($"Purchase with Id = {id} not found."); // Lève une exception personnalisée
                }

                await _purchaseService.UpdateAsync(id, updatePurchaseDto);
                return NoContent(); // Retourne un statut 204 No Content
            }
            catch (PurchaseNotFoundException ex)
            {
                return NotFound(ex.Message); // Retourne un statut 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Retourne un statut 500 en cas d'erreur
            }
        }

        // DELETE: api/purchase/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurchase(int id)
        {
            try
            {
                var purchase = await _purchaseService.GetByIdAsync(id);
                if (purchase == null)
                {
                    throw new PurchaseNotFoundException($"Purchase with Id = {id} not found."); // Lève une exception personnalisée
                }

                await _purchaseService.DeleteAsync(id); // Méthode à créer dans votre service pour supprimer l'achat
                return NoContent(); // Retourne un statut 204 No Content
            }
            catch (PurchaseNotFoundException ex)
            {
                return NotFound(ex.Message); // Retourne un statut 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Retourne un statut 500 en cas d'erreur
            }
        }

        // PUT: api/purchase/archive/{id}
        [HttpPut("archive/{id}")]
        public async Task<ActionResult<PurchaseDto>> ArchivePurchase(int id)
        {
            try
            {
                var archivedPurchase = await _purchaseService.ArchivePurchaseAsync(id);
                return Ok(archivedPurchase); // Retourne l'achat archivé
            }
            catch (PurchaseNotFoundException ex)
            {
                return NotFound(ex.Message); // Retourne un statut 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Retourne un statut 500 en cas d'erreur
            }
        }

        // GET: api/purchase/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> SearchPurchases([FromQuery] string query, [FromQuery] string sortBy = "Id", [FromQuery] bool ascending = true)
        {
            var purchases = await _purchaseService.SearchPurchasesAsync(query, sortBy, ascending);
            return Ok(purchases); // Retourne les résultats de la recherche
        }

        // GET: api/purchase/filters
        [HttpGet("filters")]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchasesByFilters(DateTime? startDate, DateTime? endDate, int? supplierId, string productName)
        {
            var purchases = await _purchaseService.GetPurchasesByFiltersAsync(startDate, endDate, supplierId, productName);
            return Ok(purchases); // Retourne les achats selon les filtres
        }
    }
}
