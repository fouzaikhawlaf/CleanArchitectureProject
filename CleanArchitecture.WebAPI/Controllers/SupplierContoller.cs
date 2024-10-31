using CleanArchitecture.UseCases.Dtos.SupplierDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace CleanArchitecture.WebAPI.Controllers
{
    [Authorize]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers()
        {
            try
            {
                var suppliers = await _supplierService.GetAllAsync();
                return Ok(suppliers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {
            try
            {
                var result = await _supplierService.GetByIdAsync(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        // POST: api/suppliers
        [HttpPost]
        public async Task<ActionResult<SupplierDto>> CreateSupplier([FromBody] CreateSupplierDto supplierDto)
        {
            try
            {
                if (supplierDto == null)
                    return BadRequest("Supplier data cannot be null.");

                // Create supplier
                var createdSupplier = await _supplierService.AddAsync(supplierDto);

                // Start workflow for the newly created supplier
              

                return CreatedAtAction(nameof(GetSupplier), new { id = createdSupplier.SupplierID }, createdSupplier);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating new supplier: {ex.Message}");
            }
        }
        // PUT: api/suppliers/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateSupplier([FromRoute] int id, [FromBody] UpdateSupplierDto supplierDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _supplierService.UpdateAsync(id, supplierDto);

                // Validate the workflow after updating the supplier
               

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating supplier: {ex.Message}");
            }
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult<SupplierDto>> DeleteSupplier(int id)
        {
            try
            {
                var supplierToDelete = await _supplierService.GetByIdAsync(id);

                if (supplierToDelete == null)
                {
                    return NotFound($"Supplier with Id = {id} not found");
                }

                await _supplierService.DeleteAsync(id);

                return Ok(supplierToDelete); // Optionally, you can return the deleted supplier.
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> SearchSuppliers([FromQuery] string query)
        {
            try
            {
                var suppliers = await _supplierService.SearchAsync(query);
                return Ok(suppliers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error searching suppliers");
            }
        }

        [HttpPut("archive/{id:int}")]
        public async Task<ActionResult<SupplierDto>> ArchiveSupplier(int id, [FromBody] string updatedBy)
        {
            try
            {
                // Archive the supplier
                await _supplierService.ArchiveSupplierAsync(id);

                // Get the archived supplier data
                var archivedSupplier = await _supplierService.GetByIdAsync(id);

                if (archivedSupplier == null)
                {
                    return NotFound($"Supplier with Id = {id} not found");
                }

                // Validate the workflow after archiving the supplier
              

                return Ok(archivedSupplier);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error archiving supplier: {ex.Message}");
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportSuppliersToPdf()
        {
            try
            {
                var pdfData = await _supplierService.ExportAllSuppliersToPdfAsync();
                return File(pdfData, "application/pdf", "Suppliers.pdf");
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error exporting suppliers to PDF");
            }
        }


        
    }
}
