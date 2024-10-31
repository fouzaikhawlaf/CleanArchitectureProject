using CleanArchitecture.UseCases.Dtos.SalesDtos;
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
                var sales = await _saleService.GetAllAsync();
                return Ok(sales);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
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

        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesHistory()
        {
            try
            {
                var salesHistory = await _saleService.GetSalesHistoryAsync();
                return Ok(salesHistory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving sales history");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesByFilters([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] int? clientId, [FromQuery] string productName)
        {
            try
            {
                var filteredSales = await _saleService.GetSalesByFiltersAsync(startDate, endDate, clientId, productName);
                return Ok(filteredSales);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving filtered sales");
            }
        }

        [HttpGet("client/{clientId:int}")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesByClientId(int clientId)
        {
            try
            {
                var clientSales = await _saleService.GetSalesByClientIdAsync(clientId);
                return Ok(clientSales);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving sales by client");
            }
        }

        [HttpGet("order/{orderClientId:int}")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesByOrderClientId(int orderClientId)
        {
            try
            {
                var sales = await _saleService.GetSalesByOrderClientIdAsync(orderClientId);
                return Ok(sales);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving sales by order client");
            }
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var sales = await _saleService.GetSalesByDateRangeAsync(startDate, endDate);
                return Ok(sales);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving sales by date range");
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesByStatus(string status)
        {
            try
            {
                var sales = await _saleService.GetSalesByStatusAsync(status);
                return Ok(sales);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving sales by status");
            }
        }

        [HttpGet("product-name/{productName}")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesByProductName(string productName)
        {
            try
            {
                var sales = await _saleService.GetSalesByProductNameAsync(productName);
                return Ok(sales);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving sales by product name");
            }
        }

        [HttpGet("export-csv")]
        public async Task<ActionResult<string>> ExportSalesToCsv()
        {
            try
            {
                var sales = await _saleService.GetAllAsync();
                if (sales == null || !sales.Any())
                {
                    return NotFound("No sales available for export.");
                }

                var filePath = await _saleService.ExportSalesToCsvAsync(sales);
                return Ok(new { filePath });
            }
            catch (Exception ex)
            {
                // Log the exception for more details
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error exporting sales to CSV: {ex.Message}");
            }
        }

    }
}
