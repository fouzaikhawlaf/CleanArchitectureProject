using CleanArchitecture.UseCases.Dtos.SalesDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
            
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SaleDto>> GetSale(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null)
            {
                return NotFound($"Sale with Id = {id} not found");
            }

            return Ok(sale);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSales()
        {
            try
            {
                var sales = await _saleService.GetAllSalesAsync();
                return Ok(sales);
            }
            catch (Exception )
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("calculate-total-amount/{saleId:int}/{clientId:int}/{productId:int}")]
        public async Task<ActionResult<decimal>> CalculateTotalAmount(int saleId, int clientId, int productId)
        {
            try
            {
                var totalAmount = await _saleService.CalculateTotalAmountAsync(saleId, clientId, productId);
                return Ok(totalAmount);
            }
            catch (Exception )
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error calculating total amount");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SaleDto>> CreateSale([FromBody] CreateSaleDto createSaleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newSale = await _saleService.AddAsync(createSaleDto);
            return CreatedAtAction(nameof(GetSale), new { id = newSale.Id }, newSale);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] UpdateSaleDto updateSaleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _saleService.UpdateAsync(id, updateSaleDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Sale with Id = {id} not found");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                await _saleService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Sale with Id = {id} not found");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> SearchSales([FromQuery] string query, [FromQuery] string sortBy = "SaleDate", [FromQuery] bool ascending = true)
        {
            var sales = await _saleService.SearchSalesAsync(query, sortBy, ascending);
            return Ok(sales);
        }

        [HttpPut("archive/{id:int}")]
        public async Task<ActionResult<SaleDto>> ArchiveSale(int id)
        {
            try
            {
                var archivedSale = await _saleService.ArchiveSaleAsync(id);

                if (archivedSale == null)
                {
                    return NotFound($"Sale with Id = {id} not found");
                }

                return Ok(archivedSale);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error archiving sale");
            }
        }
         
    }
}
