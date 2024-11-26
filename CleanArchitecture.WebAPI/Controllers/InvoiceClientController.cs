using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Dtos.InvoicesDto.InvoiceClientDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.UseCases.Dtos.InvoicesDto;

namespace CleanArchitecture.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceClientController : ControllerBase
    {
        private readonly IInvoiceClientService _invoiceClientService;

        public InvoiceClientController(IInvoiceClientService invoiceClientService)
        {
            _invoiceClientService = invoiceClientService;
        }

        // Endpoint pour générer une facture
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateInvoice([FromBody] GenerateInvoiceRequest request)
        {
            try
            {
                var invoice = _invoiceClientService.GenerateInvoiceAsync(request.OrderClientUrl, request.BonDeLivraisonUrl);
                return Ok(invoice); // Renvoie la facture générée
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur est survenue lors de la génération de la facture: {ex.Message}");
            }
        }

        // Endpoint pour exporter les factures en CSV
        [HttpGet("export-csv")]
        public IActionResult ExportInvoicesToCsv([FromQuery] IEnumerable<InvoiceClientDto> invoices)
        {
            try
            {
                var filePath = _invoiceClientService.ExportInvoicesToCsv(invoices);
                if (string.IsNullOrEmpty(filePath))
                {
                    return StatusCode(500, "Erreur lors de l'exportation des factures en CSV.");
                }

                // Retourne le fichier CSV
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "text/csv", "invoices.csv");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de l'exportation des factures en CSV: {ex.Message}");
            }
        }

        // Endpoint pour récupérer une facture par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var invoice = await _invoiceClientService.GetByIdAsync(id);
            if (invoice == null)
            {
                return NotFound("Facture non trouvée.");
            }
            return Ok(invoice);
        }

        // Endpoint pour mettre à jour une facture
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] UpdateInvoiceClientDto updateDto)
        {
            try
            {
                var existingInvoice = await _invoiceClientService.GetByIdAsync(id);
                if (existingInvoice == null)
                {
                    return NotFound("Facture non trouvée.");
                }

               await _invoiceClientService.UpdateAsync(id,updateDto);
                return NoContent(); // 204 No Content si la mise à jour est réussie
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de la mise à jour de la facture: {ex.Message}");
            }
        }

        // Endpoint pour supprimer une facture
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                var existingInvoice = await _invoiceClientService.GetByIdAsync(id);
                if (existingInvoice == null)
                {
                    return NotFound("Facture non trouvée.");
                }

                await _invoiceClientService.DeleteAsync(id);
                return NoContent(); // 204 No Content si la suppression est réussie
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur lors de la suppression de la facture: {ex.Message}");
            }
        }
    }

    // Classe de requête pour générer une facture
    public class GenerateInvoiceRequest
    {
        public string OrderClientUrl { get; set; } = string.Empty;
        public string BonDeLivraisonUrl { get; set; } = string.Empty;
    }
}
