using CleanArchitecture.UseCases.Dtos.InvoicesDto;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InvoiceSupplierController : ControllerBase
{
    private readonly IInvoiceSupplierService _invoiceSupplierService;

    // Injection de dépendances via le constructeur
    public InvoiceSupplierController(IInvoiceSupplierService invoiceSupplierService)
    {
        _invoiceSupplierService = invoiceSupplierService;
    }

    // Rechercher des factures avec un terme de recherche et des dates facultatives
    [HttpGet("search")]
    public IActionResult SearchInvoices(string searchTerm, DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {
            var invoices = _invoiceSupplierService.SearchInvoices(searchTerm, startDate, endDate);
            return Ok(invoices);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erreur lors de la recherche des factures : {ex.Message}");
        }
    }

    // Générer une facture à partir d'une commande fournisseur et d'un bon de réception
    [HttpPost("generate")]
    public IActionResult GenerateInvoice(string orderSupplierUrl, string bonDeReceptionUrl)
    {
        try
        {
            var invoice = _invoiceSupplierService.GenerateInvoiceAsync(orderSupplierUrl, bonDeReceptionUrl);
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erreur lors de la génération de la facture : {ex.Message}");
        }
    }

    [HttpPost("export/csv")]
    public IActionResult ExportInvoicesToCsv([FromBody] IEnumerable<InvoiceSupplierDto> invoices)
    {
        try
        {
            // Utilisation de la méthode pour obtenir les données CSV
            var csvData = _invoiceSupplierService.ExportInvoicesToCsv(invoices); // Cette méthode doit retourner une chaîne de caractères (les données CSV)
            var fileName = "invoices_export.csv";
            var mimeType = "text/csv";

            // Retourner le fichier CSV
            return File(new System.Text.UTF8Encoding().GetBytes(csvData), mimeType, fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erreur lors de l'exportation des factures : {ex.Message}");
        }
    }


    // Obtenir une facture par ID
    [HttpGet("{id}")]
    public IActionResult GetInvoiceById(int id)
    {
        try
        {
            var invoice = _invoiceSupplierService.GetByIdAsync(id);
            if (invoice == null)
            {
                return NotFound($"Facture avec ID {id} non trouvée.");
            }
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erreur lors de la récupération de la facture : {ex.Message}");
        }
    }

    // Supprimer une facture par ID
    [HttpDelete("{id}")]
    public IActionResult DeleteInvoice(int id)
    {
        try
        {
            _invoiceSupplierService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erreur lors de la suppression de la facture : {ex.Message}");
        }
    }
}
