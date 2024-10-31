using CleanArchitecture.Entities.Produit;
using CleanArchitecture.FrameworkAndDrivers.Exceptions;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] string sortBy = "Name", [FromQuery] bool ascending = true)
        {
            try
            {
                var products = await _productService.GetProductsAsync(sortBy, ascending);
                return Ok(products);
            }
            catch (Exception )
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto productDto)
        {
            try
            {
                if (productDto == null)
                    return BadRequest();

                var createdProduct = await _productService.AddAsync(productDto);

                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductID }, createdProduct);
            }
            catch (InvalidEntityException ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new product record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productToUpdate = await _productService.GetByIdAsync(id);
                if (productToUpdate == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                await _productService.UpdateAsync(id, productDto);
                return Ok(productDto);
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
        public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await _productService.GetByIdAsync(id);

                if (productToDelete == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                await _productService.DeleteAsync(id);

                return Ok(productToDelete); // Optionally, you can return the deleted product.
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts([FromQuery] string query, [FromQuery] string sortBy = "Name", [FromQuery] bool ascending = true)
        {
            try
            {
                var products = await _productService.SearchProductsAsync(query, sortBy, ascending);
                return Ok(products);
            }
            catch (Exception)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error searching products");
            }
        }

        [HttpPut("archive/{id:int}")]
        public async Task<ActionResult<ProductDto>> ArchiveProduct(int id)
        {
            try
            {
                _logger.LogInformation("Starting archive of product with ID {ProductId}", id);
                var archivedProduct = await _productService.ArchiveProductAsync(id);

                if (archivedProduct == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found", id);
                    return NotFound($"Product with Id = {id} not found");
                }

                _logger.LogInformation("Successfully archived product with ID {ProductId}", id);
                return Ok(archivedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error archiving product with ID {ProductId}", id);
                return BadRequest($"Error archiving product with ID {id}: {ex.Message}");
            }
        }
        [HttpGet("archived")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetArchivedProducts()
        {
            try
            {
                _logger.LogInformation("Fetching archived products");
                var archivedProducts = await _productService.GetArchivedProductsAsync();
                return Ok(archivedProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching archived products");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching archived products");
            }
        }




        [HttpGet("export/{id:int}")]
        public async Task<IActionResult> ExportProductToPdf(int id)
        {
            try
            {
                _logger.LogInformation("Starting export of product with ID {ProductId}", id);
                var pdfBytes = await _productService.ExportProductToPdfAsync(id);

                if (pdfBytes == null)
                {
                    _logger.LogWarning("No PDF bytes returned for product ID {ProductId}", id);
                    return NotFound();
                }

                _logger.LogInformation("Successfully exported product with ID {ProductId} to PDF", id);
                return File(pdfBytes, "application/pdf", $"Product_{id}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting product to PDF for product ID {ProductId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error exporting product to PDF");
            }
        }

        [HttpGet("export-all")]
        public async Task<IActionResult> ExportAllProductsToPdf()
        {
            try
            {
                _logger.LogInformation("Starting export of all products to PDF");
                var pdfBytes = await _productService.ExportAllProductsToPdfAsync();

                if (pdfBytes == null)
                {
                    _logger.LogWarning("No PDF bytes returned for products");
                    return NotFound();
                }

                _logger.LogInformation("Successfully exported all products to PDF");
                return File(pdfBytes, "application/pdf", "AllProducts.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting all products to PDF");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error exporting products to PDF");
            }
        }
    

    private void ValidateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
                throw new InvalidEntityException("Product name cannot be null or empty.");
            if (string.IsNullOrEmpty(product.Description))
                throw new InvalidEntityException("Product description cannot be null or empty.");
            if (product.Price <= 0)
                throw new InvalidEntityException("Product price must be greater than zero.");
        }
    }
}
