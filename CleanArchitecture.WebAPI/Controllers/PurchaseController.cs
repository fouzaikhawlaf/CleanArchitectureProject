using CleanArchitecture.FrameworkAndDrivers.Exceptions;
using CleanArchitecture.UseCases.Dtos.PurchaseDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchases()
        {
            try
            {
                var purchases = await _purchaseService.GetAllPurchasesAsync();
                return Ok(purchases);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchase(int id)
        {
            var purchase = await _purchaseService.GetByIdAsync(id);
            if (purchase == null)
            {
                return NotFound($"Purchase with Id = {id} not found");
            }

            return Ok(purchase);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDto>> CreatePurchase([FromBody] CreatePurchaseDto purchaseDto)
        {
            try
            {
                if (purchaseDto == null)
                    return BadRequest();

                var createdPurchase = await _purchaseService.AddAsync(purchaseDto);

                return CreatedAtAction(nameof(GetPurchase), new { id = createdPurchase.Id }, createdPurchase);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new purchase record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PurchaseDto>> UpdatePurchase([FromRoute] int id, [FromBody] UpdatePurchaseDto purchaseDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var purchaseToUpdate = await _purchaseService.GetByIdAsync(id);
                if (purchaseToUpdate == null)
                {
                    return NotFound($"Purchase with Id = {id} not found");
                }

                await _purchaseService.UpdateAsync(id, purchaseDto);
                return Ok(purchaseDto);
            }
            catch (InvalidEntityException ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePurchase(int id)
        {
            try
            {
                var purchaseToDelete = await _purchaseService.GetByIdAsync(id);
                if (purchaseToDelete == null)
                {
                    return NotFound($"Purchase with Id = {id} not found");
                }

                await _purchaseService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }

        [HttpPut("archive/{id:int}")]
        public async Task<ActionResult<PurchaseDto>> ArchivePurchase(int id)
        {
            try
            {
                var archivedPurchase = await _purchaseService.ArchivePurchaseAsync(id);

                if (archivedPurchase == null)
                {
                    return NotFound($"Purchase with Id = {id} not found");
                }

                return Ok(archivedPurchase);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error archiving purchase");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> SearchPurchases(string query, string sortBy, bool ascending)
        {
            try
            {
                var purchases = await _purchaseService.SearchPurchasesAsync(query, sortBy, ascending);
                return Ok(purchases);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error searching purchases");
            }
        }

        [HttpGet("calculateTotalAmount/{supplierId:int}/{productId:int}")]
        public async Task<ActionResult<decimal>> CalculateTotalAmount(int supplierId, int productId)
        {
            try
            {
                var totalAmount = await _purchaseService.CalculateTotalAmountAsync(supplierId, productId);
                return Ok(totalAmount);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error calculating total amount");
            }
        }
    }
}

